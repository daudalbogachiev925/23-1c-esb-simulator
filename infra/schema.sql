CREATE TABLE processed (
    id UUID PRIMARY KEY,
    number TEXT NOT NULL,
    kind TEXT NOT NULL,
    ts TIMESTAMP DEFAULT NOW()
);

CREATE TABLE failed (
    id UUID PRIMARY KEY,
    number TEXT NOT NULL,
    reason TEXT,
    ts TIMESTAMP DEFAULT NOW()
);

CREATE INDEX idx_processed_ts ON processed(ts);
CREATE INDEX idx_failed_ts ON failed(ts);
