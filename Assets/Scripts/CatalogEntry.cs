public class CatalogEntry {

    //struct variables
    private string _roomNumber;
    private string _resourceTitle;
    private bool _resourceDisplayed;
    private string _resourceDescription;
    private string _filePath;

    //constructor
    public CatalogEntry(string room, string title, bool displayed, string desc, string path)
    {
        _roomNumber = room;
        _resourceTitle = title;
        _resourceDisplayed = displayed;
        desc = _resourceDescription;
        _filePath = path;
    }
}
