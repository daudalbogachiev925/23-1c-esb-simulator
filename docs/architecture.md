# Архитектура ESB

Поток:
Publisher → RabbitMQ → Consumer → Postgres
                          ↓
                       Fault → DLQ

Retry-политика:
- exponential: 1s, 2s, 4s, 8s, 30s (5 попыток)
- delayed redelivery: 1m, 5m, 15m

Очереди:
- esb.documents
- esb.documents.retry
- esb.documents.dlq
- esb.documents.failed

Gateway: POST /documents — принять и опубликовать в шину.
