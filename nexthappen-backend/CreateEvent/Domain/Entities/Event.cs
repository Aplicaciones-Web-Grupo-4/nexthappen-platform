using System.Text.Json;
using nexthappen_backend.CreateEvent.Domain.ValueObjects;

namespace nexthappen_backend.CreateEvent.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Organizer { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal? Price { get; private set; }
    public int? Quantity { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Location { get; private set; } = string.Empty;
    public string PhotosJson { get; private set; } = "[]";
    public List<string> Photos
    {
        get
        {
            if (string.IsNullOrWhiteSpace(PhotosJson))
                return new List<string>();

            try
            {
                return JsonSerializer.Deserialize<List<string>>(PhotosJson) ?? new List<string>();
            }
            catch
            {
                return new List<string>(); // fallback si hay formato inválido
            }
        }
        private set => PhotosJson = JsonSerializer.Serialize(value);
    }

    public EventDateRange DateRange { get; private set; }

    private Event() { }
    public Event(
        string organizer,
        string title,
        string description,
        decimal? price,
        int? quantity,
        string category,
        string address,
        string location,
        IEnumerable<string> photos,
        EventDateRange dateRange)
    {
        Organizer = organizer;
        Title = title;
        Description = description;
        Price = price;
        Quantity = quantity;
        Category = category;
        Address = address;
        Location = location;
        Photos = photos.ToList();
        DateRange = dateRange;
    }
    public void UpdateDetails(
        string organizer,
        string title,
        string description,
        decimal? price,
        int? quantity,
        string category,
        string address,
        string location,
        IEnumerable<string> photos,
        EventDateRange dateRange)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("El título del evento no puede estar vacío.");
        
        if (price.HasValue && price < 0)
            throw new ArgumentException("El precio no puede ser negativo.");
        
        if (quantity.HasValue && quantity < 0)
            throw new ArgumentException("La cantidad no puede ser negativa.");
        
        if (dateRange.StartDate > dateRange.EndDate)
            throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");
        
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("La categoría no puede estar vacía.");
        
        Organizer = organizer;
        Title = title.Trim();
        Description = description?.Trim() ?? string.Empty;
        Price = price;
        Quantity = quantity;
        Category = category.Trim();
        Address = address?.Trim() ?? string.Empty;
        Location = location?.Trim() ?? string.Empty;
        Photos = photos?.ToList() ?? new List<string>();
        DateRange = dateRange;
    }
    
    #region
    public void UpdateOrganizer(string organizer) => Organizer = organizer;
    public void UpdateTitle(string title) => Title = title;
    public void UpdateDescription(string description) => Description = description;
    public void UpdatePrice(decimal? price) => Price = price;
    public void UpdateQuantity(int? quantity) => Quantity = quantity;
    public void UpdateCategory(string category) => Category = category;
    public void UpdateAddress(string address) => Address = address;
    public void UpdateLocation(string location) => Location = location;
    public void UpdatePhotos(List<string> photos) => Photos = photos;
    public void UpdateDateRange(EventDateRange range) => DateRange = range;
    #endregion
}