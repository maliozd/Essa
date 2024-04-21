using Application.Common.DTOs.InstagramApiDtos;
using Application.Common.Interfaces;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace Infrastructure.Clients
{
    public class InstaApiClient : IInstaApiClient
    {
        private readonly HttpClient _httpClient;
        public InstaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 12_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 Instagram 105.0.0.11.118 (iPhone11,8; iOS 12_3_1; en_US; en-US; scale=2.00; 828x1792; 165586599)");
        }
        public async Task<List<Node>> GetMediaNodesAsync(string username)
        {
            try
            {
                string baseUrl = "https://i.instagram.com/api/v1/users/web_profile_info/?username={0}";
                string url = string.Format(baseUrl, username);

                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var userInfoJson = await response.Content.ReadAsStringAsync();

                using JsonDocument document = JsonDocument.Parse(userInfoJson);

                JsonElement root = document.RootElement;
                JsonElement userRoot = root.GetProperty("data").GetProperty("user");

                JsonElement edges = userRoot.GetProperty("edge_owner_to_timeline_media").GetProperty("edges");
                List<Node>? returnNodeList = ExtractMediaNodes(edges);
                return returnNodeList;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to fetch data !");
                Console.ForegroundColor = default;
                Console.WriteLine(ex.Message);
                return new(); //
            }
        }

        static List<Node> ExtractMediaNodes(JsonElement mediaEdges)
        {
            List<Node> nodesToReturn = new();
            ArrayEnumerator arrayEnumerator = mediaEdges.EnumerateArray();
            foreach (JsonElement edgeElement in arrayEnumerator)
            {
                JsonElement nodeElement = edgeElement.GetProperty("node");

                Node node = new()
                {
                    DisplayUrl = nodeElement.GetProperty("display_url").GetString(),
                    Shortcode = nodeElement.GetProperty("shortcode").GetString(),
                    MediaType = nodeElement.GetProperty("__typename").GetString(),
                    Id = nodeElement.GetProperty("id").GetString(),
                    Caption = string.Empty
                };

                if (nodeElement.GetProperty("is_video").GetBoolean())
                    node.DisplayUrl = nodeElement.GetProperty("video_url").GetString();

                if (nodeElement.TryGetProperty("edge_media_to_caption", out JsonElement captionElement))
                {
                    JsonElement captionEdgeElement = captionElement.GetProperty("edges");
                    foreach (var obj in captionEdgeElement.EnumerateArray())
                        node.Caption = obj.GetProperty("node").GetProperty("text").GetString();
                }

                if (nodeElement.TryGetProperty("thumbnail_resources", out JsonElement thumbnailResources) && thumbnailResources.ValueKind != JsonValueKind.Null)
                    node.ThumbnailResources = GetThumbnailResources(thumbnailResources);

                if (nodeElement.TryGetProperty("edge_sidecar_to_children", out JsonElement edgeSidecarToChildren) && edgeSidecarToChildren.ValueKind != JsonValueKind.Null)
                {
                    List<Node>? sidecarNodes = ExtractMediaNodes(edgeSidecarToChildren.GetProperty("edges"));
                    node.WillBePost = true;
                    //nodesToReturn.AddRange(sidecarNodes);

                    sidecarNodes.ForEach(node.ChildNodes.Add);
                }
                nodesToReturn.Add(node);
            }
            return nodesToReturn;
        }
        static List<ThumbnailResource> GetThumbnailResources(JsonElement thumbnailResourcesElement)
        {
            List<ThumbnailResource> thumbnailResources = [];
            foreach (JsonElement thumbnailElement in thumbnailResourcesElement.EnumerateArray())
            {
                ThumbnailResource thumbnail = new()
                {
                    Src = thumbnailElement.GetProperty("src").GetString(),
                    ConfigWidth = thumbnailElement.GetProperty("config_width").GetInt32(),
                    ConfigHeight = thumbnailElement.GetProperty("config_height").GetInt32()
                };
                thumbnailResources.Add(thumbnail);
            }
            return thumbnailResources;
        }
    }
}
