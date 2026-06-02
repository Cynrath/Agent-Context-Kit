# AgentContextKit

AI destekli geliştirme icin offline-first repository context ve guvenlik araci.

AgentContextKit, Codex, Claude Code, Cursor, GitHub Copilot, Gemini CLI ve benzeri AI coding agent kullanan gelistiriciler icin .NET tabanli bir CLI aracidir (`ackit`). Repository yapisini analiz eder, stack sinyallerini tespit eder, guvenli agent yonerge/workflow dosyalari uretir ve public release ya da AI context export oncesi secret/PII/marka sizinti risklerini raporlar.

## Problem
AI coding agent'lar cogu projede eksik, eski veya guvensiz context ile calisir. Bu durum yanlis dosya degisikligi, production ayari sizintisi, zayif task plani, eksik test, tutarsiz agent yonergeleri ve private projenin public hale gelirken hassas bilgi sizdirmasi gibi riskler dogurur.

## Cozum
AgentContextKit lokal ve tekrarlanabilir bir workflow kurar:
- Repository yapisini ve stack sinyallerini tarar.
- Kisa bir project map uretir.
- AI agent yonerge dosyalari uretir.
- Task-first gelistirme dokumanlari olusturur.
- Riskli dosyalari ve secret benzeri icerigi raporlar.
- Var olan dosyalari varsayilan olarak ezmez.

## Kimler Kullanmalı
- AI-assisted development yapan gelistiriciler.
- Acik kaynak maintainer'lari.
- Freelance, ajans ve kucuk ekipler.
- Private repository'yi public yapmadan once temizlik yapmak isteyen ekipler.
- Codex/Claude/Cursor/Copilot icin tutarli proje context'i isteyen gelistiriciler.

## Neden Offline-first
MVP uzak AI API cagrisi yapmaz ve repository icerigini yuklemez. Bu yaklasim private kodu lokalde tutar, veri sizintisi riskini azaltir ve kisitli ortamlarda kullanimi kolaylastirir.

## Ozellikler
- `ackit init`: `.ackit/config.yml` olusturur, var olan config'i ezmez.
- `ackit scan`: stack, docs, test, CI, Docker, agent dosyalari ve riskli yolları tespit eder.
- `ackit generate`: desteklenen agent hedefleri icin context ve workflow dosyalari uretir.
- `ackit task`: `docs/tasks` altinda yapilandirilmis task dosyasi olusturur.
- `ackit redact-check`: secret/PII/marka/local path risklerini raporlar.
- `ackit doctor`: OSS ve repository saglik kontrollerini raporlar.
- `--json`: otomasyon uyumlu machine-readable JSON cikti uretir.
- English ve Turkish output/template temeli.

## Hizli Baslangic
```powershell
dotnet restore
dotnet build -c Release
dotnet run --project src/AgentContextKit.Cli -- --help
dotnet run --project src/AgentContextKit.Cli -- scan
dotnet run --project src/AgentContextKit.Cli -- scan --json
dotnet run --project src/AgentContextKit.Cli -- task "Yetki kontrollerini ekle" --lang tr
```

## CLI Komutlari
```text
ackit init [--lang en|tr] [--json]
ackit scan [--lang en|tr] [--json]
ackit generate [--target codex|claude|cursor|copilot|all] [--lang en|tr] [--json]
ackit task "<baslik>" [--lang en|tr] [--json]
ackit redact-check [--profile public-release] [--lang en|tr] [--json]
ackit doctor [--lang en|tr] [--json]
ackit version
ackit help
```

## Uretilen Dosyalar
Komuta ve hedefe gore AgentContextKit su dosyalari uretebilir:
- `AGENTS.md`
- `CLAUDE.md`
- `.cursor/rules/project.mdc`
- `.github/copilot-instructions.md`
- `docs/PROJECT_MAP.md`
- `docs/AI_WORKFLOW.md`
- `docs/SECURITY_NOTES.md`
- `docs/DEVELOPMENT_STANDARD.md`
- `docs/tasks/TASK-0001.md`
- `.codex/HANDOFF.md`
- `.codex/CONTEXT_PACK.md`

## Guvenli Davranis
- Var olan dosyalar varsayilan olarak atlanir.
- MVP otomatik secret redaction yapmaz.
- Uzak servise upload yapmaz.
- GitHub push veya NuGet publish yapmaz.
- Risk raporlari severity bazlidir: Critical, High, Medium, Low, Info.

## Lokalizasyon
Varsayilan dil English'tir. Turkish icin `--lang tr` kullanilir. Bilinmeyen dil degeri English'e duser.

## Konfigurasyon Ve JSON
`.ackit/config.yml` icin [docs/CONFIGURATION.md](docs/CONFIGURATION.md), machine-readable cikti icin [docs/JSON_OUTPUT.md](docs/JSON_OUTPUT.md) dosyasina bakin.

## Dokumantasyon
Baslangic icin [docs/DOCUMENTATION_INDEX.md](docs/DOCUMENTATION_INDEX.md) dosyasina bakin.

Onemli dokumanlar:
- [CLI Reference](docs/CLI_REFERENCE.md)
- [Examples](docs/EXAMPLES.md)
- [Configuration](docs/CONFIGURATION.md)
- [JSON Output](docs/JSON_OUTPUT.md)
- [Troubleshooting](docs/TROUBLESHOOTING.md)
- [Architecture](docs/ARCHITECTURE.md)
- [Source Hygiene](docs/SOURCE_HYGIENE.md)
- [Security Model](docs/SECURITY_MODEL.md)
- [Packaging](docs/PACKAGING.md)
- [Public Release Audit](docs/PUBLIC_RELEASE_AUDIT.md)
- [Release Validation](docs/RELEASE_VALIDATION.md)
- [Release Blockers](docs/RELEASE_BLOCKERS.md)

## Roadmap
Bkz. [docs/ROADMAP.md](docs/ROADMAP.md).

## Paketleme
Lokal paket dogrulama adimlari [docs/PACKAGING.md](docs/PACKAGING.md) ve [docs/RELEASE_VALIDATION.md](docs/RELEASE_VALIDATION.md) dosyalarinda yer alir. NuGet publish manuel maintainer aksiyonudur ve MVP otomasyonunun parcasi degildir.

Public release blocker listesi [docs/RELEASE_BLOCKERS.md](docs/RELEASE_BLOCKERS.md) dosyasinda takip edilir.

## Katki
Bkz. [CONTRIBUTING.md](CONTRIBUTING.md).

## Guvenlik
Bkz. [SECURITY.md](SECURITY.md). Public issue'larda secret, private repository icerigi veya production config paylasmayin.

## Lisans
MIT. Bkz. [LICENSE](LICENSE).
