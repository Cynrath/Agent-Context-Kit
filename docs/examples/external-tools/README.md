# External Tool Examples

These are documentation-only examples. AgentContextKit does not install, invoke, update, or trust these tools by default.

Future smoke validation must follow [Disposable External Workflow Lab](../../DISPOSABLE_EXTERNAL_WORKFLOW_LAB.md).

## Mandatory Boundary
1. Work in a disposable synthetic repository with no secrets, customer data, private URLs, or credentials.
2. Confirm the selected tool and version manually.
3. Run `ackit scan --ci` and `ackit doctor` before the external command.
4. Keep output under ignored `.ackit/external/`.
5. Review output before sharing; generated output is local-only.
6. Do not put tokens in commands, shell history, config, or examples.
7. Do not use hosted upload, telemetry, remote repository, registry-auto, or model/API modes unless a separate reviewed task explicitly permits them.

## Examples
- [Repomix local](repomix-local.md)
- [Gitingest local](gitingest-local.md)
- [Code2Prompt local](code2prompt-local.md)
- [Graphify local AST lab](graphify-local.md)
- [Gitleaks local](gitleaks-local.md)
- [Semgrep local rules](semgrep-local-rules.md)
- [Trivy filesystem](trivy-filesystem.md)
- [Secretlint local](secretlint-local.md)
- [Syft SBOM experiment](syft-sbom-experiment.md)
- [Docs quality local](docs-quality-local.md)

The commands are reviewed guidance, not a stable compatibility contract. Check the installed tool's help before use.
