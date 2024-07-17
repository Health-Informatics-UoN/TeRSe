# TeRSe

A minimal GA4GH TRS implementation.

Like, _really_ minimal.

Not bad as a reference for a simple .NET implementation of TRS.

Only serves one workflow, from local disk.

Some features not implemented.

## Missing/limited functionality

- List all tools endpoint `/tools` does not process any query parameters
    - Since the query parameters are about limiting or filtering the returned values, there seems little point in implementing them for a system that only ever serves one tool.
- Tool Descriptors are fixed to CWL
    - The use case that spawned this implementation (other than learning) was to provide a TRS endpoint for a single (configurable) CWL workflow. So at this time to simplify configuration and implementation, this is hardcoded.
- Most optional response properties are omitted.
    - This is similar to some mature implementations that choose not to engage with certain optional properties. e.g. WorkflowHub.
    - But taken to a minimal extreme.
- Tool IDs are irrelevant.
    - We only serve one tool, so as long as suitable configuration is provided, that tool will be served for ANY id requested.
    - If configuration is incomplete, the tool will report "not found" for any id.
    - responses will report the Tool id the same as requested.
- One version only
    - Similar to the above, version IDs are technically irrelevant, but are configurable for sanity.
    - This implementation does not implement a complete registry, so does not track and store tool version information - it just serves the only version of a tool as configured.
- Secondary descriptors are not identified
    - Similar to WorkflowHub, we care about the primary descriptor only.
    - Like WorkflowHub, relative paths are relative to the tool files root, NOT relative to the primary descriptor's location (which is technically what the spec requires)
    - In practice this means the `/descriptors` endpoint with relative paths actually can be used to return any individual file (not just descriptors) per its relative path obtained from the `/files` endpoint. This behaviour matches WorkflowHub.
- The `/tests` endpoint is not implemented.
    - It exists, but will always return an empty document with 200 OK. This behaviour matches WorkflowHub as far as I can tell.
    - To support this additional configuration would be needed and it's not necessary for the use case that spawned this implementation.
- The `/containerfile` endpoint is not implemented
    - It exists but will always 404 currently.
    - It's not necessary for the initial use case, but given time it may be worth exploring the implementation, and comparing against a real world implementation such as WorkflowHub or Dockstore.
- We don't currently generate file checksums
    - WorkflowHub doesn't either, even though it's a production service and this is a development / utility implementation :)
