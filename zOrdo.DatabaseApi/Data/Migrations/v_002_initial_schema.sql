------------------------------------------------------------------------------
-- MIGRATION: 001_initial_users
-- PURPOSE : Create core USERS table
------------------------------------------------------------------------------

BEGIN;

CREATE TABLE IF NOT EXISTS USERS (
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


CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);

COMMIT;
