#!/usr/bin/env bash
# python3 ./aca-py provision --wallet-type basic --seed 000000000000000000000000Steward1
# python3 ./aca-py start --inbound-transport http 0.0.0.0 8123 --admin 0.0.0.0 8124 --admin-insecure-mode --outbound-transport http

python3 ./aca-py start \
    --inbound-transport http 0.0.0.0 8123 \
    --admin 0.0.0.0 8124 --admin-insecure-mode \
    --outbound-transport http \
    --webhook-url "http://127.0.0.1:31890/v1/controller/alice" \
    --endpoint http://0.0.0.0:8123
