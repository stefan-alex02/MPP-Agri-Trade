namespace WebApp.Models.DTO;

public class ReviewDto(
    int reviewId,
    string fromName,
    string fromUsername,
    string toName,
    string toUsername,
    string description,
    int rating,
    string date) {
    public int ReviewId { get; set; } = reviewId;
    public string FromName { get; set; } = fromName;
    public string FromUsername { get; set; } = fromUsername;
    public string ToName { get; set; } = toName;
    public string ToUsername { get; set; } = toUsername;
    public string Description { get; set; } = description;
    public int Rating { get; set; } = rating;
    public string Date { get; set; } = date;
}