using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace musicplayer.report
{
	public class ReportGenerator
	{
		public string CreateReport(ReportContent content)
		{
			var doc = new HtmlDocument();

			// 1. Create the root structure
			var html = doc.CreateElement("html");
			doc.DocumentNode.AppendChild(html);

			var head = doc.CreateElement("head");
			html.AppendChild(head);

			// Add some basic styling
			var style = doc.CreateElement("style");
			style.InnerHtml = "body { font-family: sans-serif; } table { border-collapse: collapse; width: 50%; } td, th { border: 1px solid #ddd; padding: 8px; } th { text-align: left; background-color: #f2f2f2; }";
			head.AppendChild(style);

			var body = doc.CreateElement("body");
			html.AppendChild(body);

			// 2. Add a Header
			var h1 = doc.CreateElement("h1");
			h1.InnerHtml = "Music Library Report";
			body.AppendChild(h1);

			// 3. Section 1: General Stats
			var h2Stats = doc.CreateElement("h2");
			h2Stats.InnerHtml = "General Statistics";
			body.AppendChild(h2Stats);

			var ul = doc.CreateElement("ul");
			ul.AppendChild(CreateListItem(doc, "Total Artists", content.ArtistCount.ToString()));
			ul.AppendChild(CreateListItem(doc, "Total Albums", content.AlbumCount.ToString()));
			ul.AppendChild(CreateListItem(doc, "Total Songs", content.SongCount.ToString()));
			ul.AppendChild(CreateListItem(doc, "Total Listening Time", FormatTime(content.TotalListeningTime)));
			body.AppendChild(ul);

			// 4. Section 2: Popularity Metrics (Using a Table for cleanliness)
			var h2Metrics = doc.CreateElement("h2");
			h2Metrics.InnerHtml = "Engagement Metrics";
			body.AppendChild(h2Metrics);

			var table = doc.CreateElement("table");
			var headerRow = doc.CreateElement("tr");
			headerRow.AppendChild(CreateHeaderCell(doc, "Category"));
			headerRow.AppendChild(CreateHeaderCell(doc, "Name"));
			headerRow.AppendChild(CreateHeaderCell(doc, "Time / Count"));
			table.AppendChild(headerRow);

			// Artist Rows
			table.AppendChild(CreateRow(doc, "Most Popular Artist", content.MostPopularArtist, content.MostPopularArtistListeningTime));
			table.AppendChild(CreateRow(doc, "Least Popular Artist", content.LeastPopularArtist, content.LeastPopularArtistListeningTime));

			// Album Rows
			table.AppendChild(CreateRow(doc, "Most Popular Album", content.MostPopularAlbum, content.MostPopularAlbumListeningTime));
			table.AppendChild(CreateRow(doc, "Least Popular Album", content.LeastPopularAlbum, content.LeastPopularAlbumListeningTime));

			// Song Rows
			table.AppendChild(CreateRow(doc, "Most Popular Song", content.MostPopularSong, content.MostPopularSongListeningTime));
			table.AppendChild(CreateRow(doc, "Least Popular Song", content.LeastPopularSong, content.LeastPopularSongListeningTime));

			body.AppendChild(table);

			return doc.DocumentNode.OuterHtml;
		}

		// --- Helpers to reduce verbosity ---

		private HtmlNode CreateListItem(HtmlDocument doc, string label, string value)
		{
			var li = doc.CreateElement("li");
			li.InnerHtml = $"<strong>{label}:</strong> {value}";
			return li;
		}

		private HtmlNode CreateRow(HtmlDocument doc, string category, string name, int value)
		{
			var tr = doc.CreateElement("tr");

			var td1 = doc.CreateElement("td");
			td1.InnerHtml = category;
			tr.AppendChild(td1);

			var td2 = doc.CreateElement("td");
			td2.InnerHtml = name;
			tr.AppendChild(td2);

			var td3 = doc.CreateElement("td");
			td3.InnerHtml = value.ToString();
			tr.AppendChild(td3);

			return tr;
		}

		private HtmlNode CreateHeaderCell(HtmlDocument doc, string text)
		{
			var th = doc.CreateElement("th");
			th.InnerHtml = text;
			return th;
		}

		private string FormatTime(long seconds)
		{
			return TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
		}
	}
}
