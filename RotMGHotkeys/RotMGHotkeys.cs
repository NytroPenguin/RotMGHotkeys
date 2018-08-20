using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Lib_K_Relay;
using Lib_K_Relay.Utilities;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.GameData;
using Lib_K_Relay.GameData.DataStructures;

namespace RotMGHotkeys
{
    public class RotMGHotkeys : IPlugin
    {
        private Settings gui;
        private Client _client;
        private bool hotswapping = false;
        private DateTime lastSwapTime = DateTime.Now;
        public string GetAuthor()
        {
            return "NytroPenguin";
        }

        public string[] GetCommands()
        {
            return new string[] { "/hotkeys enable:disable", "invSwap on:off" };
        }

        public string GetDescription()
        {
            return "Create Hotkeys";
        }

        public string GetName()
        {
            return "Hotkeys";
        }

        public void Initialize(Proxy proxy)
        {
            gui = new Settings(this);
            PluginUtils.ShowGUI(gui);
            proxy.HookCommand("hotkeys", onCommand);
            proxy.HookPacket(PacketType.UPDATE, onUpdatePacket);
            proxy.HookPacket(PacketType.PLAYERSHOOT, onShoot);
        }

        private void onShoot(Client client, Packet packet)
        {
            if (hotswapping)
            {
                packet.Send = false;
            }
        }

        private void onUpdatePacket(Client client, Packet packet)
        {
            _client = client;
        }

        private void onCommand(Client client, string command, string[] args)
        {
            _client = client;
            if (command == "hotkeys")
            {
                if (args[0] == "on")
                {
                    gui?.Close();
                    gui = new Settings(this);
                    PluginUtils.ShowGUI(gui);
                    client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Text Hotkeys: on"));
                } else if (args[0] == "off")
                {
                    gui?.Close();
                    client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Text Hotkeys: off"));
                } 
            }
        }

        public void processHotkey(int id)
        {
            switch (id)
            {
                case 1:
                    if (Config.f1().Trim() != "")
                        sendPlayerText(Config.f1().Trim());
                    break;
                case 2:
                    if (Config.f2().Trim() != "")
                        sendPlayerText(Config.f2().Trim());
                    break;
                case 3:
                    if (Config.f3().Trim() != "")
                        sendPlayerText(Config.f3().Trim());
                    break;
                case 4:
                    if (Config.f4().Trim() != "")
                        sendPlayerText(Config.f4().Trim());
                    break;
                case 5:
                    if (Config.f5().Trim() != "")
                        sendPlayerText(Config.f5().Trim());
                    break;
                case 6:
                    if (Config.f6().Trim() != "")
                        sendPlayerText(Config.f6().Trim());
                    break;
                case 7:
                    if (Config.f7().Trim() != "")
                        sendPlayerText(Config.f7().Trim());
                    break;
                case 8:
                    if (Config.f8().Trim() != "")
                        sendPlayerText(Config.f8().Trim());
                    break;
                case 9:
                    inventorySwap(4);
                    break;
                case 10:
                    inventorySwap(5);
                    break;
                case 11:
                    inventorySwap(6);
                    break;
                case 12:
                    inventorySwap(7);
                    break;
                case 13:
                    inventorySwap(8);
                    break;
                case 14:
                    inventorySwap(9);
                    break;
                case 15:
                    inventorySwap(10);
                    break;
                case 16:
                    inventorySwap(11);
                    break;
                default:
                    break;
            }
        }

        private void sendPlayerText(string text)
        {
            PlayerTextPacket playerTextPacket = (PlayerTextPacket)Packet.Create(PacketType.PLAYERTEXT);
            playerTextPacket.Text = text.Trim();
            _client.SendToServer(playerTextPacket);
        }

        private void inventorySwap(byte itemSlot)
        {
            if (DateTime.Now < lastSwapTime.AddSeconds(2))
                return;
            lastSwapTime = DateTime.Now;
            hotswapping = true;
            byte equipSlotId = 0;
            if (_client.PlayerData.Slot[(int)itemSlot] == -1)
                return;
            switch (GameData.Items.ByID((ushort)_client.PlayerData.Slot[(int)itemSlot]).SlotType)
            {
                    case 1:
                    case 2:
                    case 3:
                    case 8:
                    case 17:
                    case 24:
                        equipSlotId = 0;
                        break;
                    case 6:
                    case 7:
                    case 14:
                        equipSlotId = 2;
                        break;
                    case 4:
                    case 5:
                    case 11:
                    case 12:
                    case 13:
                    case 15:
                    case 16:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 25:
                    case 27:
                        equipSlotId = 1;
                        break;
                    case 9:
                        equipSlotId = 3;
                        break;

                }
            InvSwapPacket invSwap = (InvSwapPacket)Packet.Create(PacketType.INVSWAP);
            invSwap.Time = _client.Time + 10;
            invSwap.Position = _client.PlayerData.Pos;

            invSwap.SlotObject1 = new SlotObject();
            invSwap.SlotObject1.ObjectId = _client.ObjectId;
            invSwap.SlotObject1.SlotId = equipSlotId;
            invSwap.SlotObject1.ObjectType = _client.PlayerData.Slot[(int)equipSlotId];

            invSwap.SlotObject2 = new SlotObject();
            invSwap.SlotObject2.ObjectId = _client.ObjectId;
            invSwap.SlotObject2.SlotId = itemSlot;
            invSwap.SlotObject2.ObjectType = _client.PlayerData.Slot[(int)itemSlot];

            _client.SendToServer(invSwap);
            PluginUtils.Delay(1000, () => {
                hotswapping = false;
            });
        }
        
    }
}
