﻿using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using RevitDBExplorer.Domain.DataModel.MemberAccessors;
using RevitDBExplorer.Domain.DataModel.Streams.Base;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.Streams
{
    internal class SchemaTypeStream : BaseStream
    {
        private static readonly IEnumerable<ISnoopableMemberTemplate> ForSchema = Enumerable.Empty<ISnoopableMemberTemplate>();

        static SchemaTypeStream()
        {
            ForSchema = new ISnoopableMemberTemplate[]
               {
                    new SnoopableMemberTemplate<Parameter, string>(typeof(Schema), "Get all elements that have entity of this schema", new Schema_GetAllElements()),
               };
            
        }


        public SchemaTypeStream()
        {
            RegisterTemplates(typeof(Schema), ForSchema);
        }
    }
}
