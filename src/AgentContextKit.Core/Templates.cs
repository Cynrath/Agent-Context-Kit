namespace AgentContextKit.Core;

public sealed class TextProvider : ITextProvider
{
    private static readonly Dictionary<string, Dictionary<string, string>> Texts = new(StringComparer.OrdinalIgnoreCase)
    {
        ["help"] = new()
        {
            ["en"] = "AgentContextKit CLI",
            ["tr"] = "AgentContextKit CLI"
        },
        ["created"] = new()
        {
            ["en"] = "created",
            ["tr"] = "olusturuldu"
        },
        ["skipped"] = new()
        {
            ["en"] = "skipped existing",
            ["tr"] = "var olan atlandi"
        },
        ["scanSummary"] = new()
        {
            ["en"] = "Scan summary",
            ["tr"] = "Tarama ozeti"
        },
        ["doctor"] = new()
        {
            ["en"] = "Doctor checks",
            ["tr"] = "Doctor kontrolleri"
        },
        ["noFindings"] = new()
        {
            ["en"] = "No risk findings.",
            ["tr"] = "Risk bulgusu yok."
        }
    };

    public string Get(string key, LanguageCode language)
    {
        if (!Texts.TryGetValue(key, out var byLanguage))
        {
            return key;
        }

        return byLanguage.TryGetValue(language.Value, out var value)
            ? value
            : byLanguage["en"];
    }
}

public sealed class TemplateRenderer : ITemplateRenderer
{
    public string Render(string templateId, LanguageCode language, IReadOnlyDictionary<string, string> values)
    {
        var template = TemplateCatalog.Get(templateId, language);
        foreach (var pair in values)
        {
            template = template.Replace("{{" + pair.Key + "}}", pair.Value, StringComparison.Ordinal);
        }

        return template;
    }
}

public static class TemplateCatalog
{
    private static readonly Dictionary<string, Dictionary<string, string>> Templates = new(StringComparer.OrdinalIgnoreCase)
    {
        ["AGENTS"] = new()
        {
            ["en"] = """
            # Agent Instructions

            ## Project
            {{ProjectName}}

            ## Stack
            {{StackList}}

            ## Repository Health
            {{HealthSummary}}

            ## Risk Summary
            {{RiskSummary}}

            ## Required Workflow
            - Read docs before editing.
            - Create or update a task file before implementation.
            - Inspect git status before edits.
            - Do not overwrite existing files without explicit approval.
            - Keep changes small, tested, and secure.
            - Update handoff and active task completion notes.

            ## Recommended Checks
            {{RecommendedChecks}}

            ## Safety Rules
            - Keep work local and offline unless explicitly approved.
            - Do not push, publish, tag, create remotes, or redact automatically.
            - Treat secret/PII/brand findings as release-blocking until reviewed.
            - Run relevant checks before completion.
            """,
            ["tr"] = """
            # Agent Yonergeleri

            ## Proje
            {{ProjectName}}

            ## Stack
            {{StackList}}

            ## Repository Sagligi
            {{HealthSummary}}

            ## Risk Ozeti
            {{RiskSummary}}

            ## Zorunlu Workflow
            - Kodlamadan once dokumanlari oku.
            - Implementation oncesi task dosyasi olustur veya guncelle.
            - Edit oncesi git durumunu incele.
            - Acik onay olmadan var olan dosyalari ezme.
            - Degisiklikleri kucuk, testli ve guvenli tut.
            - Handoff ve aktif task completion notlarini guncelle.

            ## Onerilen Kontroller
            {{RecommendedChecks}}

            ## Guvenlik Kurallari
            - Acik onay olmadan calismayi local ve offline tut.
            - Push, publish, tag, remote creation veya automatic redaction yapma.
            - Secret/PII/brand bulgularini incelenene kadar release-blocking say.
            - Tamamlamadan once ilgili kontrolleri calistir.
            """
        },
        ["CLAUDE"] = new()
        {
            ["en"] = """
            # Claude Project Context

            Use the same repository rules as AGENTS.md.

            ## Stack
            {{StackList}}

            ## Repository Health
            {{HealthSummary}}

            ## Risk Summary
            {{RiskSummary}}

            ## Recommended Checks
            {{RecommendedChecks}}
            """,
            ["tr"] = """
            # Claude Proje Context

            AGENTS.md ile ayni repository kurallarini kullan.

            ## Stack
            {{StackList}}

            ## Repository Sagligi
            {{HealthSummary}}

            ## Risk Ozeti
            {{RiskSummary}}

            ## Onerilen Kontroller
            {{RecommendedChecks}}
            """
        },
        ["CURSOR"] = new()
        {
            ["en"] = """
            # Cursor Rules

            - Respect existing architecture.
            - Use task-first workflow.
            - Keep generated changes safe and reviewable.
            - Check repository health and risk summary before editing.

            ## Recommended Checks
            {{RecommendedChecks}}
            """,
            ["tr"] = """
            # Cursor Kurallari

            - Mevcut mimariye uy.
            - Task-first workflow kullan.
            - Uretilen degisiklikleri guvenli ve incelenebilir tut.
            - Edit oncesi repository sagligi ve risk ozetini kontrol et.

            ## Onerilen Kontroller
            {{RecommendedChecks}}
            """
        },
        ["COPILOT"] = new()
        {
            ["en"] = """
            # Copilot Instructions

            Prefer minimal, tested, secure changes that follow the project docs and task files.

            Repository health:
            {{HealthSummary}}

            Recommended checks:
            {{RecommendedChecks}}
            """,
            ["tr"] = """
            # Copilot Yonergeleri

            Proje dokumanlari ve task dosyalarina uyan minimal, testli ve guvenli degisiklikleri tercih et.

            Repository sagligi:
            {{HealthSummary}}

            Onerilen kontroller:
            {{RecommendedChecks}}
            """
        },
        ["PROJECT_MAP"] = new()
        {
            ["en"] = """
            # Project Map

            Generated: {{GeneratedAt}}

            ## Stack
            {{StackList}}

            ## Files
            {{FileList}}
            """,
            ["tr"] = """
            # Proje Haritasi

            Uretim zamani: {{GeneratedAt}}

            ## Stack
            {{StackList}}

            ## Dosyalar
            {{FileList}}
            """
        },
        ["AI_WORKFLOW"] = new()
        {
            ["en"] = "# AI Workflow\n\n1. Read README, architecture, security, and the active task.\n2. Create or update a task before implementation.\n3. Inspect git status and preserve user changes.\n4. Make small, focused changes.\n5. Run relevant checks.\n6. Update docs, task completion notes, and handoff.\n7. Commit a logical unit of work.\n\n## Recommended Checks\n{{RecommendedChecks}}\n",
            ["tr"] = "# AI Workflow\n\n1. README, architecture, security ve aktif task dosyasini oku.\n2. Implementation oncesi task olustur veya guncelle.\n3. Git durumunu incele ve user degisikliklerini koru.\n4. Kucuk ve odakli degisiklikler yap.\n5. Ilgili kontrolleri calistir.\n6. Docs, task completion notlari ve handoff guncelle.\n7. Mantikli bir is birimi commit et.\n\n## Onerilen Kontroller\n{{RecommendedChecks}}\n"
        },
        ["SECURITY_NOTES"] = new()
        {
            ["en"] = "# Security Notes\n\n- Do not commit secrets.\n- Review production configuration before public release.\n- Keep redaction checks report-only unless explicitly approved.\n- Treat Critical and High findings as blockers until reviewed.\n\n## Current Risk Summary\n{{RiskSummary}}\n",
            ["tr"] = "# Guvenlik Notlari\n\n- Secret commit etme.\n- Public release oncesi production config dosyalarini incele.\n- Acik onay olmadan redaction kontrollerini sadece rapor olarak tut.\n- Critical ve High bulgulari incelenene kadar blocker say.\n\n## Guncel Risk Ozeti\n{{RiskSummary}}\n"
        },
        ["DEVELOPMENT_STANDARD"] = new()
        {
            ["en"] = "# Development Standard\n\n- Task-first.\n- Docs-first.\n- Security-first.\n- Tests before completion.\n- Update handoff after major changes.\n- Keep public release actions maintainer-only.\n\n## Recommended Checks\n{{RecommendedChecks}}\n",
            ["tr"] = "# Gelistirme Standardi\n\n- Task-first.\n- Docs-first.\n- Security-first.\n- Tamamlama oncesi test.\n- Buyuk degisikliklerden sonra handoff guncelle.\n- Public release aksiyonlarini maintainer-only tut.\n\n## Onerilen Kontroller\n{{RecommendedChecks}}\n"
        },
        ["TASK"] = new()
        {
            ["en"] = """
            # {{TaskNumber}}: {{TaskTitle}}

            ## Purpose

            ## Scope

            ## Out of scope

            ## Affected files

            ## Data/database impact

            ## Security impact

            ## Permission/auth impact

            ## Localization impact

            ## UX impact

            ## Logging/audit impact

            ## Acceptance criteria

            ## Test steps

            ## Risks

            ## Rollback plan

            ## Completion notes
            """,
            ["tr"] = """
            # {{TaskNumber}}: {{TaskTitle}}

            ## Amac

            ## Kapsam

            ## Kapsam disi

            ## Etkilenen dosyalar

            ## Veri tabani etkisi

            ## Guvenlik etkisi

            ## Yetki/auth etkisi

            ## Lokalizasyon etkisi

            ## UX etkisi

            ## Log/audit etkisi

            ## Kabul kriterleri

            ## Test adimlari

            ## Riskler

            ## Geri alma plani

            ## Tamamlama notlari
            """
        },
        ["HANDOFF"] = new()
        {
            ["en"] = "# Handoff\n\nGenerated: {{GeneratedAt}}\n\n## Current Repository\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n\n## Repository Health\n{{HealthSummary}}\n\n## Risk Summary\n{{RiskSummary}}\n\n## Recommended Checks\n{{RecommendedChecks}}\n",
            ["tr"] = "# Handoff\n\nUretim zamani: {{GeneratedAt}}\n\n## Repository\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n\n## Repository Sagligi\n{{HealthSummary}}\n\n## Risk Ozeti\n{{RiskSummary}}\n\n## Onerilen Kontroller\n{{RecommendedChecks}}\n"
        },
        ["CONTEXT_PACK"] = new()
        {
            ["en"] = "# Context Pack\n\n## Project\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n\n## Repository Health\n{{HealthSummary}}\n\n## Risk Summary\n{{RiskSummary}}\n\n## Recommended Checks\n{{RecommendedChecks}}\n\n## Files\n{{FileList}}\n",
            ["tr"] = "# Context Pack\n\n## Proje\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n\n## Repository Sagligi\n{{HealthSummary}}\n\n## Risk Ozeti\n{{RiskSummary}}\n\n## Onerilen Kontroller\n{{RecommendedChecks}}\n\n## Dosyalar\n{{FileList}}\n"
        }
    };

    public static string Get(string templateId, LanguageCode language)
    {
        if (!Templates.TryGetValue(templateId, out var byLanguage))
        {
            throw new InvalidOperationException($"Template not found: {templateId}");
        }

        return byLanguage.TryGetValue(language.Value, out var template)
            ? template
            : byLanguage["en"];
    }
}
