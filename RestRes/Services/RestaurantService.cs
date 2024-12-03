using MongoDB.Bson;
using RestRes.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace RestRes.Services
{
  public class RestaurantService : IRestaurantService
  {
    private readonly RestaurantReservationDbContext _restaurantDbContext;
    public RestaurantService(RestaurantReservationDbContext restaurantDbContext)
    {
      _restaurantDbContext = restaurantDbContext;
    }
    public void AddRestaurant(Restaurant restaurant)
    {
      _restaurantDbContext.Restaurants.Add(restaurant);
      _restaurantDbContext.ChangeTracker.DetectChanges();
      Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);
        _restaurantDbContext.SaveChanges();
      }

    public void DeleteRestaurant(Restaurant restaurant)
    {
      var restaurantToDelete = _restaurantDbContext.Restaurants.Where(c => c.Id == restaurant.Id).FirstOrDefault();
      if (restaurantToDelete != null)
      {
        _restaurantDbContext.Restaurants.Remove(restaurantToDelete);
        _restaurantDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);
        _restaurantDbContext.SaveChanges();
      }
      else {
        throw new ArgumentException("The restaurant to delete was not found");
      }
    }

    public void EditRestaurant(Restaurant restaurant)
    {
      var restaurantToUpdate = _restaurantDbContext.Restaurants.FirstOrDefault(c => c.Id == restaurant.Id);
      if (restaurantToUpdate != null)
      {
        restaurantToUpdate.name = restaurant.name;
        restaurantToUpdate.cuisine = restaurant.cuisine;
        restaurantToUpdate.borough = restaurant.borough;
        _restaurantDbContext.Restaurants.Update(restaurantToUpdate);
        _restaurantDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);
        _restaurantDbContext.SaveChanges();
      }
      else {
        throw new ArgumentException("The restaurant to update was not found");
      }
    }

    public IEnumerable<Restaurant> GetRestaurants()
    {
      return _restaurantDbContext.Restaurants.OrderByDescending(c => c.Id).Take(20).AsNoTracking().AsEnumerable();
    }

    public Restaurant? GetRestaurantById(ObjectId id)
    {
      return _restaurantDbContext.Restaurants.FirstOrDefault(c => c.Id == id);
    }
    }
}
