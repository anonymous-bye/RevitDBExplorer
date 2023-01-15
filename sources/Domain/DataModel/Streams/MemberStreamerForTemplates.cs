﻿using System;
using System.Collections.Generic;
using System.Linq;
using RevitDBExplorer.Domain.DataModel.MemberTemplates.Base;
using RevitDBExplorer.Domain.DataModel.Streams.Base;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.Streams
{
    internal static class MemberStreamerForTemplates
    {
        private static readonly Dictionary<Type, List<ISnoopableMemberTemplate>> forTypes = new();

        public static void Init()
        {

        }


        static MemberStreamerForTemplates()
        {
            var memberTemplateFactories = GetAllInstancesThatImplement<IHaveMemberTemplates>();

            foreach (var factory in memberTemplateFactories)
            {
                foreach (var template in factory.GetTemplates())
                {
                    RegisterTemplate(template);
                }
            }
        }
        private static IEnumerable<T> GetAllInstancesThatImplement<T>() where T : class
        {
            var type = typeof(T);
            var types = type.Assembly.GetTypes().Where(p => type.IsAssignableFrom(p) && !p.IsInterface).ToList();
            var instances = types.Select(x => Activator.CreateInstance(x) as T);
            return instances;
        }
        private static void RegisterTemplate(ISnoopableMemberTemplate template)
        {
            if (!forTypes.TryGetValue(template.ForType, out List<ISnoopableMemberTemplate> list))
            {
                list = new List<ISnoopableMemberTemplate>();
                forTypes[template.ForType] = list;
            }
            list.Add(template);
        }


        public static IEnumerable<MemberDescriptor> Stream(object snoopableObject)
        {            
            var objectType = snoopableObject.GetType();
            foreach (var keyValue in forTypes)
            {
                if (keyValue.Key.IsAssignableFrom(objectType))
                {
                    foreach (var template in keyValue.Value)
                    {
                        if (template.CanBeUsedWith(snoopableObject))
                        {
                            
                            yield return template.Data;
                        }
                    }
                }
            }
        }
    }

    internal interface IHaveMemberTemplates
    {
        IEnumerable<ISnoopableMemberTemplate> GetTemplates();
    }
}