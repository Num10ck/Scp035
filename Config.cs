using Exiled.API.Interfaces;
using System.ComponentModel;
using UnityEngine;

namespace PluginUtils.Plugins.SCP035
{
    internal class Config : IConfig
    {
        [Description("Включить или Выключить плагин?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Не используется.")]
        public bool Debug { get; set; } = false;
        [Description("Изменяет комнату SCP-079 на кастомную комнату SCP-035.")]
        public string Schematic { get; set; } = "SCP035Room";
        [Description("Надо ли перед началом раунда открыть двери комнаты SCP-079 (иначе игроки даже не узнают о SCP-035)")]
        public bool IsOpenDoors { get; set; } = true;
        [Description("Надо ли очистить все предметы в комнате (иначе игроки смогут взять оружие). Я пока выключу.")]
        public bool IsNeededDelete { get; set; } = false;
        [Description("Количество ХП игрока")]
        public static int Health { get; set; } = 200;
        [Description("Напишите позицию для маски относительно выбранной комнаты.")]
        public static Vector3 Position { get; set; } = new Vector3(-0.7f, -2.5f, -9.5f);
    }
}
