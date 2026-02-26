#!/usr/bin/env bash
set -euo pipefail

DB_PATH="${1:-Mission08_Team0203/Tasks.sqlite}"

if ! command -v sqlite3 >/dev/null 2>&1; then
  echo "Error: sqlite3 is not installed or not in PATH."
  exit 1
fi

if [[ ! -f "$DB_PATH" ]]; then
  echo "Error: database file not found at '$DB_PATH'"
  exit 1
fi

sqlite3 "$DB_PATH" <<'SQL'
BEGIN TRANSACTION;

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Finish mission write-up', '2026-02-28', 1, 'School', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Finish mission write-up');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Pay utility bill', '2026-03-01', 1, 'Home', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Pay utility bill');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Plan next week schedule', '2026-03-05', 2, 'Work', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Plan next week schedule');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Read chapter 9', '2026-03-07', 2, 'School', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Read chapter 9');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Reply to non-urgent emails', '2026-03-02', 3, 'Work', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Reply to non-urgent emails');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Organize desk supplies', '2026-03-10', 3, 'Home', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Organize desk supplies');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Browse new podcast episodes', '2026-03-12', 4, 'Home', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Browse new podcast episodes');

INSERT INTO Tasks (Title, DueDate, Quadrant, Category, Completed)
SELECT 'Watch conference recap', '2026-03-15', 4, 'Church', 0
WHERE NOT EXISTS (SELECT 1 FROM Tasks WHERE Title = 'Watch conference recap');

COMMIT;
SQL

echo "Seed complete. Current task count:"
sqlite3 "$DB_PATH" "SELECT COUNT(*) FROM Tasks;"
