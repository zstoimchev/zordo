#!/bin/bash

################################################################################
# zOrdo Database Migration Runner
# Usage: ./db_versioning.sh
# Description: Executes all SQL migrations in the Migrations folder
################################################################################

set -e  # Exit on error

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DB_FILE="$SCRIPT_DIR/zordo.db"
MIGRATIONS_DIR="$SCRIPT_DIR/Migrations"

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

echo "================================================================================"
echo -e "${BLUE}zOrdo Database Migration Runner${NC}"
echo "================================================================================"
echo "Database: $DB_FILE"
echo "Migrations: $MIGRATIONS_DIR"
echo ""

# Check if migrations directory exists
if [ ! -d "$MIGRATIONS_DIR" ]; then
    echo -e "${RED}❌ Error: Migrations directory not found: $MIGRATIONS_DIR${NC}"
    exit 1
fi

# Check if sqlite3 is installed
if ! command -v sqlite3 &> /dev/null; then
    echo -e "${RED}❌ Error: sqlite3 is not installed${NC}"
    echo "Install with: sudo apt-get install sqlite3"
    exit 1
fi

# Create database file if it doesn't exist
if [ ! -f "$DB_FILE" ]; then
    echo -e "${YELLOW}📁 Database file does not exist. Creating: $DB_FILE${NC}"
    touch "$DB_FILE"
fi

# Get list of migration files sorted by name
MIGRATION_FILES=$(find "$MIGRATIONS_DIR" -name "*.sql" -type f | sort)

if [ -z "$MIGRATION_FILES" ]; then
    echo -e "${YELLOW}⚠️  No migration files found in $MIGRATIONS_DIR${NC}"
    exit 0
fi

# Counter for applied migrations
APPLIED_COUNT=0
SKIPPED_COUNT=0
FAILED_COUNT=0

# Execute each migration
for migration_file in $MIGRATION_FILES; do
    filename=$(basename "$migration_file")
    migration_name="${filename%.sql}"
    
    echo "--------------------------------------------------------------------------------"
    echo -e "${BLUE}Processing: $filename${NC}"
    
    # Check if migration was already applied
    already_applied=$(sqlite3 "$DB_FILE" \
        "SELECT COUNT(*) FROM db_version WHERE version_name = '$migration_name' 2>/dev/null" \
        2>/dev/null || echo "0")
    
    if [ "$already_applied" = "1" ]; then
        echo -e "${YELLOW}⏭️  Already applied (skipping)${NC}"
        ((SKIPPED_COUNT++))
        continue
    fi
    
    # Execute migration
    if sqlite3 "$DB_FILE" < "$migration_file" 2>&1; then
        echo -e "${GREEN}✅ Successfully applied${NC}"
        ((APPLIED_COUNT++))
    else
        echo -e "${RED}❌ Failed to apply${NC}"
        ((FAILED_COUNT++))
        # Continue with other migrations even if one fails
    fi
done

echo "================================================================================"
echo -e "${GREEN}Migration Summary:${NC}"
echo "  ✅ Applied: $APPLIED_COUNT"
echo "  ⏭️  Skipped: $SKIPPED_COUNT"
echo "  ❌ Failed: $FAILED_COUNT"
echo ""

# Show current database state
echo "================================================================================"
echo -e "${BLUE}Database Version History:${NC}"
echo "================================================================================"

sqlite3 -header -box "$DB_FILE" "SELECT 
    version_name,
    description,
    applied_on_utc,
    CASE WHEN success = 1 THEN '✅' ELSE '❌' END as status
FROM db_version 
ORDER BY version_id;"

echo ""
echo "================================================================================"
echo -e "${BLUE}Database Tables:${NC}"
echo "================================================================================"

sqlite3 -header -box "$DB_FILE" "SELECT 
    name as table_name,
    (SELECT COUNT(*) FROM sqlite_master sm WHERE sm.type='index' AND sm.tbl_name = m.name) as index_count
FROM sqlite_master m
WHERE type='table' AND name NOT LIKE 'sqlite_%'
ORDER BY name;"

echo ""
echo "================================================================================"
echo -e "${GREEN}✅ Migration process completed!${NC}"
echo "================================================================================"