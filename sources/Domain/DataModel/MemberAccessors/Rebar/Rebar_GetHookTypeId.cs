﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.MemberAccessors
{
    internal class Rebar_GetHookTypeId : MemberAccessorByType<Rebar>, IHaveFactoryMethod
    {
        protected override IEnumerable<LambdaExpression> HandledMembers { get { yield return (Rebar x) => x.GetHookTypeId(7); } }
        IMemberAccessor IHaveFactoryMethod.Create() => new Rebar_GetHookTypeId();


        protected override bool CanBeSnoooped(Document document, Rebar rebar) => true;
        protected override string GetLabel(Document document, Rebar rebar) => "[Element]";
        protected override IEnumerable<SnoopableObject> Snooop(Document document, Rebar rebar)
        {
            for (int i = 0; i < 2; ++i)
            {
                var result = rebar.GetHookTypeId(i);
                yield return new SnoopableObject(null, document, $"{i}", new[] { new SnoopableObject(result, document) });
            }
        }
    }
}
