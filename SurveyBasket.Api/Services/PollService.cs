
using System.Threading.Tasks;
using SurveyBasket.Api.Persistence;

namespace SurveyBasket.Api.Services
{
    public class PollService(ApplicationDbContext context) : IPollService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Polls.AsNoTracking().ToListAsync();
        }
        public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken )
        {
            return await _context.Polls.FindAsync(id);
        }

        public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken)
        {

            await _context.Polls.AddAsync(poll,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

           
            return poll;

        }

        public async Task<bool> UpdateAsync(int id,Poll poll, CancellationToken cancellationToken )
        {
           var currentPoll =await GetAsync(id,cancellationToken);
            if (currentPoll is null) return false;
            currentPoll.Title = poll.Title;
            currentPoll.Summary = poll.Summary;
            currentPoll.IsPublished = poll.IsPublished;
            currentPoll.StartsAt = poll.StartsAt;
            currentPoll.EndsAt = poll.EndsAt;
             _context.Polls.Update(currentPoll);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        { 
            var poll = await GetAsync(id, cancellationToken);
            if (poll is null) return false;
            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync(cancellationToken);
            return true;

        }

        public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
        {
           var poll = await GetAsync(id, cancellationToken);
            if (poll is null) return false;
            poll.IsPublished = !poll.IsPublished;
            
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
