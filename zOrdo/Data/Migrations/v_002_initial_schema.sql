------------------------------------------------------------------------------
-- MIGRATION: 001_initial_schema
-- PURPOSE : Create core application tables
------------------------------------------------------------------------------

BEGIN;

CREATE TABLE IF NOT EXISTS Users (
    ID              INTEGER PRIMARY KEY AUTOINCREMENT,
    FIRST_NAME      TEXT NOT NULL,
    MIDDLE_NAME     TEXT,
    LAST_NAME       TEXT NOT NULL,
    EMAIL           TEXT NOT NULL UNIQUE,
    PASSWORD_HASH   TEXT,
    INSERTED_ON_UTC TEXT NOT NULL,
    UPDATED_ON_UTC  TEXT,
    DELETED_ON_UTC  TEXT,
    DELETED_BY      TEXT
);

CREATE TABLE IF NOT EXISTS Tasks (
    ID                  INTEGER PRIMARY KEY AUTOINCREMENT,
    TITLE               TEXT NOT NULL,
    DESCRIPTION         TEXT NOT NULL,
    PRIORITY            INTEGER NOT NULL,
    DUE_DATE_UTC        TEXT NOT NULL,
    COMPLETED_ON_UTC    TEXT,
    STATUS              INTEGER NOT NULL,
    INSERTED_ON_UTC     TEXT NOT NULL,
    UPDATED_ON_UTC      TEXT,
    DELETED_ON_UTC      TEXT,
    DELETED_BY          TEXT
);

CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);

COMMIT;
