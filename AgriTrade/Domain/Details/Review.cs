using Domain.Users;

namespace Domain.Details;

public class Review: Entity<int> {
    public Consumer From { get; private set; } = null!;
    public Producer To { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int Rating { get; private set; }
    public DateTime Date { get; private set; }

    public Review() : base(default) {
    }

    public Review(int id, Consumer from, Producer to, string description, int rating, DateTime date) : base(id) {
        From = from;
        To = to;
        Description = description;
        Rating = rating;
        Date = date;
    }
}