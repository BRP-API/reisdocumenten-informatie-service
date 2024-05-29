#!/bin/bash

docker compose \
    -f .docker/db.yml \
    -f .docker/reisdocumenten-data-service.yml \
    -f .docker/reisdocumenten-informatie-service.yml \
    up -d
