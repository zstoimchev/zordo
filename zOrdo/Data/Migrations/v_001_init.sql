------------------------------------------------------------------------------
-- MIGRATION: 000_init
-- DESCRIPTION: Initialize database version tracking system
-- AUTHOR: Zhivko Stoimchev
-- DATE: 2026-02-02
------------------------------------------------------------------------------

BEGIN TRANSACTION;

-- Create version tracking table
CREATE TABLE IF NOT EXISTS db_version (
    id              INTEGER PRIMARY KEY AUTOINCREMENT,
    version_name    TEXT    NOT NULL    UNIQUE,
    applied_on_utc  TEXT    NOT NULL    DEFAULT (datetime('now'))
);

-- Record this initialization
INSERT OR IGNORE INTO db_version (version_name) VALUES ('000_init');

COMMIT;

-- Show result
SELECT
    '000_init' as migration,
    CASE WHEN changes() > 0 THEN '✅ APPLIED' ELSE '⏭️  SKIPPED' END as status,
    datetime('now') as executed_at;

------------------------------------------------------------------------------