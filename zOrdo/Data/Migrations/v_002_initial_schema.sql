------------------------------------------------------------------------------
-- MIGRATION: 001_initial_schema
-- PURPOSE : Create core application tables
------------------------------------------------------------------------------

BEGIN;

CREATE TABLE IF NOT EXISTS Users (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName       TEXT NOT NULL,
    MiddleName      TEXT,
    LastName        TEXT NOT NULL,
    Email           TEXT NOT NULL UNIQUE,
    PasswordHash    TEXT,
    InsertedOnUtc   TEXT NOT NULL,
    UpdatedOnUtc    TEXT,
    DeletedOnUtc    TEXT,
    DeletedBy       TEXT
);

CREATE TABLE IF NOT EXISTS Tasks (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    Title           TEXT NOT NULL,
    Description     TEXT NOT NULL,
    Priority        INTEGER NOT NULL,
    DueDateUtc      TEXT NOT NULL,
    CompletedOnUtc  TEXT,
    Status          INTEGER NOT NULL,
    InsertedOnUtc   TEXT NOT NULL,
    UpdatedOnUtc    TEXT,
    DeletedOnUtc    TEXT,
    DeletedBy       TEXT
);

CREATE INDEX IF NOT EXISTS idx_users_email ON Users(Email);

COMMIT;
