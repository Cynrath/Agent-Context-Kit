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

            ## Rules
            - Read docs before editing.
            - Create or update a task file before implementation.
            - Do not overwrite existing files without explicit approval.
            - Keep changes small, tested, and secure.
            - Run relevant checks before completion.
            """,
            ["tr"] = """
            # Agent Yonergeleri

            ## Proje
            {{ProjectName}}

            ## Stack
            {{StackList}}

            ## Kurallar
            - Kodlamadan once dokumanlari oku.
            - Implementation oncesi task dosyasi olustur veya guncelle.
            - Acik onay olmadan var olan dosyalari ezme.
            - Degisiklikleri kucuk, testli ve guvenli tut.
            - Tamamlamadan once ilgili kontrolleri calistir.
            """
        },
        ["CLAUDE"] = new()
        {
            ["en"] = "# Claude Project Context\n\nUse the same repository rules as AGENTS.md.\n\n{{StackList}}\n",
            ["tr"] = "# Claude Proje Context\n\nAGENTS.md ile ayni repository kurallarini kullan.\n\n{{StackList}}\n"
        },
        ["CURSOR"] = new()
        {
            ["en"] = "# Cursor Rules\n\n- Respect existing architecture.\n- Use task-first workflow.\n- Keep generated changes safe and reviewable.\n",
            ["tr"] = "# Cursor Kurallari\n\n- Mevcut mimariye uy.\n- Task-first workflow kullan.\n- Uretilen degisiklikleri guvenli ve incelenebilir tut.\n"
        },
        ["COPILOT"] = new()
        {
            ["en"] = "# Copilot Instructions\n\nPrefer minimal, tested, secure changes that follow the project docs and task files.\n",
            ["tr"] = "# Copilot Yonergeleri\n\nProje dokumanlari ve task dosyalarina uyan minimal, testli ve guvenli degisiklikleri tercih et.\n"
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
            ["en"] = "# AI Workflow\n\n1. Read docs.\n2. Create or update a task.\n3. Inspect git status.\n4. Make small changes.\n5. Run tests.\n6. Update handoff.\n",
            ["tr"] = "# AI Workflow\n\n1. Dokumanlari oku.\n2. Task olustur veya guncelle.\n3. Git durumunu incele.\n4. Kucuk degisiklikler yap.\n5. Testleri calistir.\n6. Handoff guncelle.\n"
        },
        ["SECURITY_NOTES"] = new()
        {
            ["en"] = "# Security Notes\n\n- Do not commit secrets.\n- Review production configuration before public release.\n- Keep redaction checks report-only unless explicitly approved.\n",
            ["tr"] = "# Guvenlik Notlari\n\n- Secret commit etme.\n- Public release oncesi production config dosyalarini incele.\n- Acik onay olmadan redaction kontrollerini sadece rapor olarak tut.\n"
        },
        ["DEVELOPMENT_STANDARD"] = new()
        {
            ["en"] = "# Development Standard\n\n- Task-first.\n- Docs-first.\n- Security-first.\n- Tests before completion.\n",
            ["tr"] = "# Gelistirme Standardi\n\n- Task-first.\n- Docs-first.\n- Security-first.\n- Tamamlama oncesi test.\n"
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
            ["en"] = "# Handoff\n\nGenerated: {{GeneratedAt}}\n\n## Current repository\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n",
            ["tr"] = "# Handoff\n\nUretim zamani: {{GeneratedAt}}\n\n## Repository\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n"
        },
        ["CONTEXT_PACK"] = new()
        {
            ["en"] = "# Context Pack\n\n## Project\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n\n## Files\n{{FileList}}\n",
            ["tr"] = "# Context Pack\n\n## Proje\n{{ProjectName}}\n\n## Stack\n{{StackList}}\n\n## Dosyalar\n{{FileList}}\n"
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
