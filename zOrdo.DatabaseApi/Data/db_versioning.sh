#!/bin/bash

################################################################################
# zOrdo Database Migration Runner
# Usage: ./db_versioning.sh
# Description: Executes all SQL migrations in the Migrations folder
################################################################################

set -e

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DB_FILE="$SCRIPT_DIR/zordo.db"
MIGRATIONS_DIR="$SCRIPT_DIR/Migrations"

# Colors
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
BLUE='\033[0;34m'
NC='\033[0m'

echo "================================================================================"
echo -e "${BLUE}zOrdo Database Migration Runner${NC}"
echo "================================================================================"
echo "Database: $DB_FILE"
echo "Migrations: $MIGRATIONS_DIR"
echo ""

# Checks
command -v sqlite3 >/dev/null || { echo "❌ sqlite3 not installed"; exit 1; }
[ -d "$MIGRATIONS_DIR" ] || { echo "❌ Migrations dir missing"; exit 1; }

[ -f "$DB_FILE" ] || touch "$DB_FILE"

# Ensure version table exists (bootstrap safety)
sqlite3 "$DB_FILE" <<'SQL'
CREATE TABLE IF NOT EXISTS DB_VERSION (
    VERSION_ID      INTEGER PRIMARY KEY AUTOINCREMENT,
    VERSION_NAME    TEXT    NOT NULL UNIQUE,
    APPLIED_ON_UTC  TEXT    NOT NULL,
    SUCCESS         INTEGER NOT NULL CHECK (success IN (0,1))
);
SQL

APPLIED_COUNT=0
SKIPPED_COUNT=0
FAILED_COUNT=0

for migration_file in $(find "$MIGRATIONS_DIR" -name "*.sql" | sort); do
    filename=$(basename "$migration_file")
    migration_name="${filename%.sql}"

    echo "--------------------------------------------------------------------------------"
    echo -e "${BLUE}Processing: $filename${NC}"

    already_applied=$(sqlite3 "$DB_FILE" \
        "SELECT COUNT(*) FROM DB_VERSION WHERE VERSION_NAME='$migration_name';")

    if [ "$already_applied" = "1" ]; then
        echo -e "${YELLOW}⏭️  Already applied${NC}"
        ((++SKIPPED_COUNT))
        continue
    fi

    if sqlite3 "$DB_FILE" < "$migration_file"; then
        sqlite3 "$DB_FILE" \
            "INSERT INTO DB_VERSION (VERSION_NAME, APPLIED_ON_UTC, SUCCESS)
             VALUES ('$migration_name', datetime('now'), 1);"
        echo -e "${GREEN}✅ Successfully applied${NC}"
        ((++APPLIED_COUNT))
    else
        sqlite3 "$DB_FILE" \
            "INSERT INTO DB_VERSION (VERSION_NAME, APPLIED_ON_UTC, SUCCESS)
             VALUES ('$migration_name', datetime('now'), 0);"
        echo -e "${RED}❌ Failed to apply${NC}"
        ((++FAILED_COUNT))
    fi
done

echo "================================================================================"
echo -e "${GREEN}Migration Summary:${NC}"
echo "  ✓ Applied: $APPLIED_COUNT"
echo "  ⏭ Skipped: $SKIPPED_COUNT"
echo "  ❌ Failed: $FAILED_COUNT"
echo ""

echo "================================================================================"
echo -e "${BLUE}Database Version History:${NC}"
echo "================================================================================"

sqlite3 -header -box "$DB_FILE" "
SELECT
    version_name,
    applied_on_utc,
    CASE WHEN success = 1 THEN '✓' ELSE '❌' END AS status
FROM db_version
ORDER BY version_id;
"

echo ""
echo -e "${GREEN}✅ Migration process completed!${NC}"
