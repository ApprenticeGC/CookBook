using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
// using Serilog.Sinks.Unity3D;

public static class GameLogSinkExtensions
{
    private const string DefaultDebugOutputTemplate = "[{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static LoggerConfiguration GameLog(
        this LoggerSinkConfiguration sinkConfiguration,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        string outputTemplate = DefaultDebugOutputTemplate,
        IFormatProvider formatProvider = null,
        LoggingLevelSwitch levelSwitch = null)
    {
        if (sinkConfiguration == null) throw new ArgumentNullException(nameof(sinkConfiguration));
        if (outputTemplate == null) throw new ArgumentNullException(nameof(outputTemplate));

        var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
        return sinkConfiguration.GameLog(formatter, restrictedToMinimumLevel, levelSwitch);
    }

    public static LoggerConfiguration GameLog(
        this LoggerSinkConfiguration sinkConfiguration,
        ITextFormatter formatter,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch levelSwitch = null)
    {
        if (sinkConfiguration == null) throw new ArgumentNullException(nameof(sinkConfiguration));
        if (formatter == null) throw new ArgumentNullException(nameof(formatter));

        return sinkConfiguration.Sink(new GameLogSink(formatter), restrictedToMinimumLevel, levelSwitch);
    }
}
