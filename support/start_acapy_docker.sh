#!/usr/bin/env bash
PORTS="5000:5000 8000:8000 8123:8123 8124:8124 31890:31890" ./scripts/run_docker start --inbound-transport http 0.0.0.0 8123 --admin 0.0.0.0 8124 --admin-insecure-mode --outbound-transport http --webhook-url "http://127.0.0.1:31890/v1/controller"
