#!/bin/sh
docker create --name protobuf-builder protobuf
docker cp protobuf-builder:/proto/generated ./
docker rm -f protobuf-builder