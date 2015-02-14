using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}