public class CatalogEntry {

    //class variables
    public string roomNumber;
    public string resourceTitle;
    public bool resourceDisplayed;
    public string resourceDescription;
    public string filePath;

    //constructor
    public CatalogEntry(string room, string title, bool displayed, string desc, string path)
    {
        roomNumber = room;
        resourceTitle = title;
        resourceDisplayed = displayed;
        resourceDescription = desc;
        filePath = path;
    }
}
