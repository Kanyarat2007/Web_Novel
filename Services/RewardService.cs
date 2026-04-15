using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Web_Novel.Models;

namespace Web_Novel.Services
{
    public class RewardService
    {
        private readonly WebnoveldbContext _db;

        public RewardService(WebnoveldbContext db)
        {
            _db = db;
        }

        // ─────────────────────────────
        // 🎁 สมัครใหม่
        // ─────────────────────────────
        public void GiveRegisterReward(int userId)
        {
            var exists = _db.UserRewards.Any(x =>
                x.UserId == userId &&
                x.RewardType == "register");

            if (exists)
                return;

            AddCoinTransaction(
                userId,
                5,
                "reward",
                "สมัครสมาชิกใหม่",
                "register"
            );
        }

        // ─────────────────────────────
        // 🔥 Login Streak
        // ─────────────────────────────
        public void HandleLoginStreak(int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var streak = _db.Loginstreaks
                .FirstOrDefault(x => x.UserId == userId);

            if (streak == null)
            {
                streak = new Loginstreak
                {
                    UserId = userId,
                    CurrentStreak = 1,
                    LongestStreak = 1,
                    LastLoginDate = today
                };

                _db.Loginstreaks.Add(streak);
            }
            else if (streak.LastLoginDate != today)
            {
                streak.CurrentStreak =
                    streak.LastLoginDate.AddDays(1) == today
                    ? streak.CurrentStreak + 1
                    : 1;

                streak.LastLoginDate = today;

                if (streak.CurrentStreak > streak.LongestStreak)
                    streak.LongestStreak = streak.CurrentStreak;
            }
            else
            {
                return;
            }

            int reward = CalculateStreakReward(streak.CurrentStreak);

            AddCoinTransaction(
                userId,
                reward,
                "reward",
                $"Login Streak Day {streak.CurrentStreak}",
                "streak"
            );
        }

        // ─────────────────────────────
        // 🎁 Daily Reward
        // ─────────────────────────────
        public bool ClaimDailyReward(int userId)
        {
            var today = DateTime.Today;

            var exists = _db.UserRewards.Any(x =>
                x.UserId == userId &&
                x.RewardType == "daily" &&
                x.CreatedAt.Date == today);

            if (exists)
                return false;

            AddCoinTransaction(
                userId,
                1,
                "reward",
                "Daily Reward",
                "daily"
            );

            return true;
        }

        // ─────────────────────────────
        // 💰 เพิ่มเหรียญ (ใช้ร่วมกัน)
        // ─────────────────────────────
        private void AddCoinTransaction(
    int userId,
    int amount,
    string type,
    string note,
    string rewardType = null)
        {
            var wallet = _db.UserCoins
                .FirstOrDefault(x => x.UserId == userId);

            if (wallet == null)
            {
                wallet = new UserCoin
                {
                    UserId = userId,
                    Balance = 0,
                    UpdatedAt = DateTime.Now
                };

                _db.UserCoins.Add(wallet);
                _db.SaveChanges();
            }

            int before = wallet.Balance;
            int after = before + amount;

            // 💰 update wallet (source of truth)
            wallet.Balance = after;
            wallet.UpdatedAt = DateTime.Now;

            // 📜 log transaction
            _db.Cointransactions.Add(new Cointransaction
            {
                UserId = userId,
                Amount = amount,
                Type = type,
                Note = note,
                BalanceBefore = before,
                BalanceAfter = after,
                CreatedAt = DateTime.Now
            });

            // 🎁 optional reward log
            if (!string.IsNullOrEmpty(rewardType))
            {
                _db.UserRewards.Add(new UserReward
                {
                    UserId = userId,
                    RewardType = rewardType,
                    CoinAmount = amount,
                    Description = note,
                    CreatedAt = DateTime.Now
                });
            }

            _db.SaveChanges();
        }
        
        // ─────────────────────────────
        // 🧠 คำนวณ reward streak
        // ─────────────────────────────
        public int CalculateStreakReward(int day)
        {
            if (day % 7 == 0) return 5; // bonus ทุก 7 วัน
            if (day >= 3) return 2;
            return 1;
        }
    }
}