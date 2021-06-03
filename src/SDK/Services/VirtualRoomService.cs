using System;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk.Services
{
    public class VirtualRoomService
    {
        private UrlTemplate template;
        private RestClient restClient;

        public VirtualRoomService(RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
            template = new UrlTemplate(baseUrl);
        }

        /// <summary>
        /// Gets the virtual room.
        /// 
        /// </summary>
        /// <returns>Virtual Room.</returns>
        public VirtualRoom GetVirtualRoom(PackageId packageId)
        {
            String path = template.UrlFor(UrlTemplate.VIRTUAL_ROOM_CONFIG_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            String stringResponse;
            try
            {
                stringResponse = restClient.Get(path);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not get VirtualRoom." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not get VirtualRoom." + " Exception: " + e.Message, e);
            }

            OneSpanSign.API.VirtualRoom VirtualRoom = JsonConvert.DeserializeObject<OneSpanSign.API.VirtualRoom>(stringResponse);
            VirtualRoomConverter converter = new VirtualRoomConverter(VirtualRoom);
            return converter.ToSDKVirtualRoom();
        }

        /// <summary>
        /// Update Virtual Room for account.
        /// </summary>
        /// <param name="VirtualRoom">VirtualRoom.</param>
        public void SetVirtualRoom(PackageId packageId, VirtualRoom VirtualRoom)
        {
            String path = template.UrlFor(UrlTemplate.VIRTUAL_ROOM_CONFIG_PATH)
                .Replace("{packageId}", packageId.Id)
                .Build();

            VirtualRoomConverter converter = new VirtualRoomConverter(VirtualRoom);
            String VirtualRoomJson = JsonConvert.SerializeObject(converter.ToAPIVirtualRoom());

            try
            {
                restClient.Put(path, VirtualRoomJson);
            }
            catch (OssServerException e)
            {
                throw new OssServerException("Could not update VirtualRoom" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e)
            {
                throw new OssException("Could not update VirtualRoom" + " Exception: " + e.Message, e);
            }
        }
    }
}
