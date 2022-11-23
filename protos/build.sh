OUT_DIR="./generated"
    
find src/*.proto | xargs protoc \
    --plugin="protoc-gen-ts=${PROTOC_GEN_TS_PATH}" \
    --plugin="protoc-gen-grpc=${PROTOC_GEN_CSHARP_PATH}" \
    --csharp_out="${OUT_DIR}/csharp" \
    --grpc_out="$OUT_DIR/csharp" \
    --grpc-web_out=import_style=typescript,mode=grpcweb:"$OUT_DIR/js"