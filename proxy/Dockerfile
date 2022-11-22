FROM golang:1.16-alpine3.13

RUN apk add --no-cache curl git ca-certificates && \
  rm -rf /var/lib/apt/lists/*

ARG VERSION=0.14.0

WORKDIR /tmp

RUN curl -sS https://raw.githubusercontent.com/golang/dep/master/install.sh | sh
RUN wget https://github.com/improbable-eng/grpc-web/archive/v$VERSION.tar.gz

WORKDIR /go/src/github.com/improbable-eng/

RUN tar -zxf /tmp/v$VERSION.tar.gz -C .
RUN mv grpc-web-$VERSION grpc-web

WORKDIR /go/src/github.com/improbable-eng/grpc-web

RUN dep ensure && \
  go env -w GO111MODULE=auto && \
  go install ./go/grpcwebproxy

ADD ./etc/localhost.crt /etc
ADD ./etc/localhost.key /etc

ENTRYPOINT [ "/bin/sh", "-c", "exec /go/bin/grpcwebproxy \
  --backend_addr=host.docker.internal:9090 \
  --run_tls_server=false \
  --allow_all_origins " ]
