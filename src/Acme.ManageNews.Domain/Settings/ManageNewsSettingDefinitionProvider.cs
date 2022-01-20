using Volo.Abp.Settings;

namespace Acme.ManageNews.Settings;

public class ManageNewsSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ManageNewsSettings.MySetting1));
    }
}
