using System;
using System.Reflection;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}