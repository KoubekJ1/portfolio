using musicplayer.dataobjects;

namespace musicplayer.dao
{
	public interface ISongDAO : IDAO<Song>
	{
		int? UploadSongData(byte[] data);

		void AddListeningTime(long time, int songID);
	}
}
