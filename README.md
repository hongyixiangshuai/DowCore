# DowCore
最完美的.net core 开发平台


[KanbanPlugin(ID = "EB041D2398AC471FB20922395697C33B"
     , PluginName = "注塑生产实况看板"
     , PluginDesc = "注塑生产实况看板"
     , PluginType = PluginCategory.WPF
     , PluginUrl = "MES.Plugin.KanbanMonitor;component/Views/ProductionKanban.xaml"
     , ServiceUrl = ""
     , EditorUrl = "")]
     
     [KanbanPluginParam(ParamIndex = 1, ParamName = "Title", ParamDesc = "看板标题", ParamType = typeof(string), IsRequired = true, DefaultValue = "注塑生产实况看板", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 2, ParamName = "interval", ParamDesc = "刷新时间（秒/s）", ParamType = typeof(int), IsRequired = true, DefaultValue = "15", EnumStr = "")]
    //[KanbanPluginParam(ParamIndex = 3, ParamName = "time", ParamDesc = "时间范围（小时/h）", ParamType = typeof(int), IsRequired = true, DefaultValue = "24", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 4, ParamName = "showcolumn", ParamDesc = "展示列", ParamType = typeof(int), IsRequired = true, DefaultValue = "6", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 5, ParamName = "showrow", ParamDesc = "展示行", ParamType = typeof(int), IsRequired = true, DefaultValue = "4", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 6, ParamName = "workcentercode", ParamDesc = "工作中心代码", ParamType = typeof(string), IsRequired = false, DefaultValue = "", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 7, ParamName = "workshopcode", ParamDesc = "车间代码", ParamType = typeof(string), IsRequired = false, DefaultValue = "", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 8, ParamName = "firstinspectiontime", ParamDesc = "首检时间（小时/h)", ParamType = typeof(int), IsRequired = false, DefaultValue = "2", EnumStr = "")]
    [KanbanPluginParam(ParamIndex = 9, ParamName = "nointerval", ParamDesc = "未巡检时间（分钟/m)", ParamType = typeof(string), IsRequired = false, DefaultValue = "2", EnumStr = "")]
