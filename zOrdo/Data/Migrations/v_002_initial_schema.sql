------------------------------------------------------------------------------
-- MIGRATION: 001_initial_schema
-- DESCRIPTION: Create Users and Tasks tables with indexes
-- AUTHOR: system
-- DATE: 2026-02-02
------------------------------------------------------------------------------

BEGIN TRANSACTION;

-- Users Table
CREATE TABLE IF NOT EXISTS Users (
    Id                INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName         TEXT    NOT NULL,
    MiddleName        TEXT,
    LastName          TEXT    NOT NULL,
    Email             TEXT    NOT NULL UNIQUE,
    PasswordHash      TEXT,
    InsertedOnUtc     TEXT    NOT NULL,
    UpdatedOnUtc      TEXT,
    DeletedOnUtc      TEXT,
    DeletedBy         TEXT
);

-- Tasks Table
CREATE TABLE IF NOT EXISTS Tasks (
    Id                INTEGER PRIMARY KEY AUTOINCREMENT,
    Title             TEXT    NOT NULL,
    Description       TEXT    NOT NULL,
    Priority          INTEGER NOT NULL,
    DueDateUtc        TEXT    NOT NULL,
    CompletedOnUtc    TEXT,
    Status            INTEGER NOT NULL,
    InsertedOnUtc     TEXT    NOT NULL,
    UpdatedOnUtc      TEXT,
    DeletedOnUtc      TEXT,
    DeletedBy         TEXT
);

-- WHERE indexes (SELECT optimization)
CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);

-- Record version
INSERT OR IGNORE INTO db_version (version_name, description, applied_by) VALUES ('001_initial_schema');

COMMIT;

-- Show result
SELECT
    '001_initial_schema' as migration,
    CASE WHEN changes() > 0 THEN '✅ APPLIED' ELSE '⏭️  SKIPPED' END as status,
    datetime('now') as executed_at;

------------------------------------------------------------------------------