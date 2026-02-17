------------------------------------------------------------------------------
-- MIGRATION: 001_initial_todoItems
-- PURPOSE : Create core TODO_ITEMS table
------------------------------------------------------------------------------

BEGIN;

CREATE TABLE IF NOT EXISTS TODO_ITEMS (
    ID                  INTEGER PRIMARY KEY AUTOINCREMENT,
    USER_ID             INTEGER NOT NULL,
    TITLE               TEXT NOT NULL,
    DESCRIPTION         TEXT NOT NULL,
    PRIORITY            INTEGER NOT NULL,
    STATUS              INTEGER NOT NULL,
    DUE_DATE_UTC        TEXT NOT NULL,
    COMPLETED_ON_UTC    TEXT,
    INSERTED_ON_UTC     TEXT NOT NULL,
    UPDATED_ON_UTC      TEXT,
    DELETED_ON_UTC      TEXT,
    DELETED_BY          TEXT,
    FOREIGN KEY (USER_ID) REFERENCES USERS(ID)
);


CREATE INDEX IF NOT EXISTS idx_todo_items_user_id ON TODO_ITEMS(USER_ID);

COMMIT;
