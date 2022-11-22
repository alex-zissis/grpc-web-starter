OUT_DIR="./generated"
    
find src/*.proto | xargs protoc \
    --plugin="protoc-gen-ts=${PROTOC_GEN_TS_PATH}" \
    --plugin="protoc-gen-grpc=${PROTOC_GEN_CSHARP_PATH}" \
    --js_out="import_style=commonjs,binary:${OUT_DIR}/js" \
    --ts_out="${OUT_DIR}/js" \
    --csharp_out="${OUT_DIR}/csharp" \
    --grpc_out="$OUT_DIR/csharp"