using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ipl_project
{
	class Program
    {
	    public const int MATCH_ID=0; 
	    public const int MATCH_SEASON=1; 
	    public const int MATCH_CITY=2; 
	    public const int MATCH_DATE=3; 
	    public const int MATCH_TEAM1=4; 
	    public const int MATCH_TEAM2=5; 
	    public const int MATCH_TOSS_WINNER=6; 
	    public const int MATCH_TOSS_DECISION=7; 
	    public const int MATCH_RESULT=8; 
	    public const int MATCH_DL_APPLIED=9; 
	    public const int MATCH_WINNER=10; 
	    public const int MATCH_WIN_BY_RUNS=11; 
	    public const int MATCH_WIN_BY_WICKETS=12; 
	    public const int MATCH_PLAYER_OF_MATCH=13; 
	    public const int MATCH_VENUE=14; 
	    public const int MATCH_UMPIRE1=15; 
	    public const int MATCH_UMPIRE2=16; 
	
	    public const int DELIVERY_ID=0; 
	    public const int DELIVERY_INNING=1; 
	    public const int DELIVERY_BATTING_TEAM=2; 
	    public const int DELIVERY_BOWLING_TEAM=3; 
	    public const int DELIVERY_OVER=4; 
	    public const int DELIVERY_BALL=5; 
	    public const int DELIVERY_BATSMAN=6; 
	    public const int DELIVERY_NON_STRIKER=7; 
	    public const int DELIVERY_BOWLER=8; 
	    public const int DELIVERY_IS_SUPER_OVER=9; 
	    public const int DELIVERY_WIDE_RUNS=10; 
	    public const int DELIVERY_BYE_RUNS=11; 
	    public const int DELIVERY_LEG_BYE_RUNS=12; 
	    public const int DELIVERY_NO_BALL_RUNS=13; 
	    public const int DELIVERY_PENALTY_RUNS=14; 
	    public const int DELIVERY_BATSMAN_RUNS=15; 
	    public const int DELIVERY_EXTRA_RUNS=16; 
	    public const int DELIVERY_TOTAL_RUNS=17; 
	    public const int DELIVERY_PLAYER_DISMISSED=18; 
	    public const int DELIVERY_DISMISSAL_KIND=19; 
	    public const int DELIVERY_FIELDER=20; 
	    public const int TOTAL_BALLS=0; 
	    public const int TOTAL_RUNS=1;

        public static void Main(string[] args){
	        List<Match> matches=getMatchesData();
            List<Delivery> deliveries=getDeliveriesData();
		
			getMatchesPlayedPerYear(matches);
		
            getMatchesWonPerTeam(matches);
            
			getExtraRunsConcededPerTeamIn2016(matches,deliveries);
            
	        getTopEconomicalBowlersIn2015(matches,deliveries);
                
	        getTossWinVsMatchWinRelation(matches,deliveries,false,false);

        }

        static List<Match> getMatchesData() {
	        List<Match> matches = new List<Match>();
	        StreamReader matchesCsv = new StreamReader(new FileStream("matches.csv",FileMode.OpenOrCreate));
	        matchesCsv.ReadLine();
	        String line = null;
	        while ((line = matchesCsv.ReadLine()) != null) {
		        Match match = new Match();
		        string[] fields = line.Split(',');
		        match.setId(Convert.ToInt32(fields[MATCH_ID]));
		        match.setSeason(Convert.ToInt32(fields[MATCH_SEASON]));
		        match.setCity(fields[MATCH_CITY]);
		        match.setDate(DateTime.Parse(fields[MATCH_DATE]));
		        match.setTeam1(fields[MATCH_TEAM1]);
		        match.setTeam2(fields[MATCH_TEAM2]);
		        match.setTossWinner(fields[MATCH_TOSS_WINNER]);
		        match.setTossDecision(fields[MATCH_TOSS_DECISION]);
		        match.setResult(fields[MATCH_RESULT]);
		        match.setDlApplied(Convert.ToInt32(fields[MATCH_DL_APPLIED]));
		        match.setWinner(fields[MATCH_WINNER]);
		        match.setWinByRuns(Convert.ToInt32(fields[MATCH_WIN_BY_RUNS]));
		        match.setWinByWickets(Convert.ToInt32(fields[MATCH_WIN_BY_WICKETS]));
		        match.setPlayerOfMatch(fields[MATCH_PLAYER_OF_MATCH]);
		        match.setVenue(fields[MATCH_VENUE]);
		        match.setUmpire1(MATCH_UMPIRE1<fields.Length?fields[MATCH_UMPIRE1]:"");
		        match.setUmpire2(MATCH_UMPIRE2<fields.Length?fields[MATCH_UMPIRE2]:"");

		        matches.Add(match);
	        }
	        return matches;
        }
        static List<Delivery> getDeliveriesData() {
	        List<Delivery> deliveries = new List<Delivery>();
	        StreamReader deliveriesCsv = new StreamReader(new FileStream("deliveries.csv",FileMode.Open));
	        deliveriesCsv.ReadLine();
	        String line = null;
	        while ((line = deliveriesCsv.ReadLine()) != null) {
		        Delivery delivery = new Delivery();
		        String[] fields=line.Split(',');
		        delivery.setMatchId(Convert.ToInt32(fields[DELIVERY_ID]));
		        delivery.setInning(Convert.ToInt32(fields[DELIVERY_INNING]));
		        delivery.setBattingTeam(fields[DELIVERY_BATTING_TEAM]);
		        delivery.setBowlingTeam(fields[DELIVERY_BOWLING_TEAM]);
		        delivery.setOver(Convert.ToInt32(fields[DELIVERY_OVER]));
		        delivery.setBall(Convert.ToInt32(fields[DELIVERY_BALL]));
		        delivery.setBatsman(fields[DELIVERY_BATSMAN]);
		        delivery.setNonStriker(fields[DELIVERY_NON_STRIKER]);
		        delivery.setBowler(fields[DELIVERY_BOWLER]);
		        delivery.setIsSuperOver(Convert.ToInt32(fields[DELIVERY_IS_SUPER_OVER]));
		        delivery.setWideRuns(Convert.ToInt32(fields[DELIVERY_WIDE_RUNS]));
		        delivery.setWideRuns(Convert.ToInt32(fields[DELIVERY_WIDE_RUNS]));
		        delivery.setByeRuns(Convert.ToInt32(fields[DELIVERY_BYE_RUNS]));
		        delivery.setLegByeRuns(Convert.ToInt32(fields[DELIVERY_LEG_BYE_RUNS]));
		        delivery.setNoBallRuns(Convert.ToInt32(fields[DELIVERY_NO_BALL_RUNS]));
		        delivery.setPenaltyRuns(Convert.ToInt32(fields[DELIVERY_PENALTY_RUNS]));
		        delivery.setBatsmanRuns(Convert.ToInt32(fields[DELIVERY_BATSMAN_RUNS]));
		        delivery.setExtraRuns(Convert.ToInt32(fields[DELIVERY_EXTRA_RUNS]));
		        delivery.setTotalRuns(Convert.ToInt32(fields[DELIVERY_TOTAL_RUNS]));
		        delivery.setPlayerDismissed(DELIVERY_PLAYER_DISMISSED<fields.Length?fields[DELIVERY_PLAYER_DISMISSED]:"");
		        delivery.setDismissalKind(DELIVERY_DISMISSAL_KIND<fields.Length?fields[DELIVERY_DISMISSAL_KIND]:"");
		        delivery.setFielder(DELIVERY_FIELDER<fields.Length?fields[DELIVERY_FIELDER]:"");

		        deliveries.Add(delivery);
	        }
	        return deliveries;
        }
        
        static void getMatchesPlayedPerYear(List<Match> matches) {
	        SortedDictionary<int, int> matchesPlayedPerYear = new SortedDictionary<int, int>();
	        foreach (Match match in matches) {
		        int key = Convert.ToInt32(match.getDate().Year);
		        if (matchesPlayedPerYear.ContainsKey(key))
		        {
			        matchesPlayedPerYear[key] += 1;
		        }
		        else
		        {
			        matchesPlayedPerYear.Add(key,1);
		        }
	        }
	        foreach (KeyValuePair<int,int> matchPerYear in matchesPlayedPerYear) {
		        Console.WriteLine(matchPerYear.Key + "---" +matchPerYear.Value);
	        }
        }

	    static void getMatchesWonPerTeam(List<Match> matches) {
	         SortedDictionary<string, int> matchesWonPerTeam = new  SortedDictionary<string, int>();
	        foreach (Match match in matches) {
		        String key = match.getWinner();
		        if (matchesWonPerTeam.ContainsKey(key))
		        {
			        matchesWonPerTeam[key] += 1;
		        }
		        else
		        {
			        matchesWonPerTeam.Add(key,1);
		        }
	        }
	        foreach (KeyValuePair<String,int> teamWinDetails in matchesWonPerTeam) {
		        Console.WriteLine(teamWinDetails.Key + "---" +teamWinDetails.Value);
	        }
        }
		
	    static void getExtraRunsConcededPerTeamIn2016(List<Match>matches,List<Delivery> deliveries) {
	         SortedDictionary<string, int> extraRunsConcededPerTeam = new SortedDictionary<string, int>();
	         foreach (Delivery delivery in deliveries) {
		         if (matches[delivery.getMatchId() - 1].getDate().Year == 2016) {
			         String key = delivery.getBowlingTeam();
			         int currentValue = delivery.getExtraRuns();
			         int MapValue = 0;
			         if (extraRunsConcededPerTeam.ContainsKey(key))
			         {
				         MapValue = extraRunsConcededPerTeam[key];
				         extraRunsConcededPerTeam[key] = currentValue + MapValue;
			         }
			         else
			         {
				         extraRunsConcededPerTeam.Add(key, 0);
			         }
		         }
	         }
	         foreach (KeyValuePair<String,int> teamExtraRuns in extraRunsConcededPerTeam) {
		         Console.WriteLine(teamExtraRuns.Key + "---" +teamExtraRuns.Value);
	         }
         }

	    static void getTopEconomicalBowlersIn2015(List<Match>matches,List<Delivery> deliveries) {
	         SortedDictionary<string, int[]> bowlersDetails = new SortedDictionary<string, int[]>();
	         foreach (Delivery delivery in deliveries) {
		         if (matches[delivery.getMatchId() - 1].getDate().Year == 2015) {
			         String key = delivery.getBowler();
			         int runs = delivery.getWideRuns() + delivery.getNoBallRuns() + delivery.getBatsmanRuns()
			                    + delivery.getExtraRuns();
			         if (bowlersDetails.ContainsKey(key)) {
				         int[] bowlerEconamyDetails = bowlersDetails[key];
				         bowlerEconamyDetails[TOTAL_BALLS]++;
				         bowlerEconamyDetails[TOTAL_RUNS]+= runs;
			         } else {
				         bowlersDetails.Add(key, new int[] {1,runs});
			         }
		         }
	         }
	         SortedDictionary<string, double> topEconomicalBowler = new SortedDictionary<string, double>();
	         foreach (KeyValuePair<String,int[]> bowlerDetail in bowlersDetails) {
		         int[] bowlerEconamyDetails = bowlerDetail.Value;
		         double economy = bowlerEconamyDetails[1] / (bowlerEconamyDetails[0] / 6.0);
		         topEconomicalBowler[bowlerDetail.Key]=economy;
	         }
	         List<String> bowlerNames = topEconomicalBowler.Keys.ToList();
	         List<Double> bowlerEconomies = topEconomicalBowler.Values.ToList();
	         for(int i=0;i<bowlerEconomies.Count-1;i++) {
		         for (int j=i+1;j<bowlerEconomies.Count;j++) {
			         if(bowlerEconomies[i]>bowlerEconomies[j]) {
				         double economyTemp=bowlerEconomies[i];
				         bowlerEconomies[i]=bowlerEconomies[j];
				         bowlerEconomies[j]=economyTemp;
				         String nameTemp=bowlerNames[i];
				         bowlerNames[i]=bowlerNames[j];
				         bowlerNames[j]=nameTemp;
			         }
		         }
	         }
	         for(int i=0;i<bowlerNames.Count;i++) {
		         Console.WriteLine("{0:}-----{1:N3}",bowlerNames[i],bowlerEconomies[i]);
	         }
         }

         static void getTossWinVsMatchWinRelation(List<Match>matches,List<Delivery> deliveries,bool tossWin, bool matchWin) {
	         SortedDictionary<string, int> tossWinVsMatchWinRelations = new SortedDictionary<string, int>();
	         foreach (Match match in matches) {
		         String teamA = match.getTeam1();
		         String teamB = match.getTeam2();
		         String tossWinner = match.getTossWinner();
		         String matchWinner = match.getWinner();
		         String selectedName = null;
		         if (tossWin && matchWin && tossWinner.Equals(matchWinner)) {
			         selectedName = matchWinner;
		         } else if (tossWin && !matchWin && !tossWinner.Equals(matchWinner)) {
			         selectedName = tossWinner;
		         } else if (!tossWin && matchWin && !tossWinner.Equals(matchWinner)) {
			         selectedName = matchWinner;
		         } else if (!tossWin && !matchWin && tossWinner.Equals(matchWinner)) {
			         selectedName = tossWinner.Equals(teamA) ? teamB : teamA;
		         }
		         if (selectedName == null)
			         continue;
		         String key = selectedName;
		         if (tossWinVsMatchWinRelations.ContainsKey(key))
		         {
			         tossWinVsMatchWinRelations[key] += 1;
		         }
		         else
		         {
			         tossWinVsMatchWinRelations.Add(key,1);
		         }
	         }
	         foreach (KeyValuePair<String,int> tossWinVsMatchWinRelation in tossWinVsMatchWinRelations) {
		         Console.WriteLine(tossWinVsMatchWinRelation.Key + "---" +tossWinVsMatchWinRelation.Value);
	         }
         }

    }

     public class Match{
		private int id;
		private int season;
		private String city; 
		DateTime date;
		private String team1;
		private String team2;
		private String tossWinner;
		private String tossDecision;
		private String result;
		private int dlApplied;
		private String winner;
		private int winByRuns;
		private int winByWickets;
		private String playerOfMatch;
		private String venue;
		private String umpire1;
		private String umpire2;
		public int getId() {
			return id;
		}
		public void setId(int id) {
			this.id = id;
		}
		public int getSeason() {
			return season;
		}
		public void setSeason(int season) {
			this.season = season;
		}
		public String getCity() {
			return city;
		}
		public void setCity(String city) {
			this.city = city;
		}
		public DateTime getDate() {
			return date;
		}
		public void setDate(DateTime date) {
			this.date = date;
		}
		public String getTeam1() {
			return team1;
		}
		public void setTeam1(String team1) {
			this.team1 = team1;
		}
		public String getTeam2() {
			return team2;
		}
		public void setTeam2(String team2) {
			this.team2 = team2;
		}
		public String getTossWinner() {
			return tossWinner;
		}
		public void setTossWinner(String tossWinner) {
			this.tossWinner = tossWinner;
		}
		public String getTossDecision() {
			return tossDecision;
		}
		public void setTossDecision(String tossDecision) {
			this.tossDecision = tossDecision;
		}
		public String getResult() {
			return result;
		}
		public void setResult(String result) {
			this.result = result;
		}
		public int getDlApplied() {
			return dlApplied;
		}
		public void setDlApplied(int dlApplied) {
			this.dlApplied = dlApplied;
		}
		public String getWinner() {
			return winner;
		}
		public void setWinner(String winner) {
			this.winner = winner;
		}
		public int getWinByRuns() {
			return winByRuns;
		}
		public void setWinByRuns(int winByRuns) {
			this.winByRuns = winByRuns;
		}
		public int getWinByWickets() {
			return winByWickets;
		}
		public void setWinByWickets(int winByWickets) {
			this.winByWickets = winByWickets;
		}
		public String getPlayerOfMatch() {
			return playerOfMatch;
		}
		public void setPlayerOfMatch(String playerOfMatch) {
			this.playerOfMatch = playerOfMatch;
		}
		public String getVenue() {
			return venue;
		}
		public void setVenue(String venue) {
			this.venue = venue;
		}
		public String getUmpire1() {
			return umpire1;
		}
		public void setUmpire1(String umpire1) {
			this.umpire1 = umpire1;
		}
		public String getUmpire2() {
			return umpire2;
		}
		public void setUmpire2(String umpire2) {
			this.umpire2 = umpire2;
		}

     }

     class Delivery 
     {
		private int matchId;
		private int inning;
		private int over;
		private int ball;
		private int isSuperOver;
		private int wideRuns;
		private int byeRuns;
		private int legByeRuns;
		private int noBallRuns;
		private int penaltyRuns;
		private int batsmanRuns;
		private int extraRuns;
		private int totalRuns;
		private String battingTeam;
		private String bowlingTeam;
		private String batsman;
		private String nonStriker;
		private String bowler;
		private String playerDismissed;
		private String dismissalKind;
		private String fielder;
		public int getMatchId() {
			return matchId;
		}
		public void setMatchId(int matchId) {
			this.matchId = matchId;
		}
		public int getInning() {
			return inning;
		}
		public void setInning(int inning) {
			this.inning = inning;
		}
		public int getOver() {
			return over;
		}
		public void setOver(int over) {
			this.over = over;
		}
		public int getBall() {
			return ball;
		}
		public void setBall(int ball) {
			this.ball = ball;
		}
		public int getIsSuperOver() {
			return isSuperOver;
		}
		public void setIsSuperOver(int isSuperOver) {
			this.isSuperOver = isSuperOver;
		}
		public int getWideRuns() {
			return wideRuns;
		}
		public void setWideRuns(int wideRuns) {
			this.wideRuns = wideRuns;
		}
		public int getByeRuns() {
			return byeRuns;
		}
		public void setByeRuns(int byeRuns) {
			this.byeRuns = byeRuns;
		}
		public int getLegByeRuns() {
			return legByeRuns;
		}
		public void setLegByeRuns(int legByeRuns) {
			this.legByeRuns = legByeRuns;
		}
		public int getNoBallRuns() {
			return noBallRuns;
		}
		public void setNoBallRuns(int noBallRuns) {
			this.noBallRuns = noBallRuns;
		}
		public int getPenaltyRuns() {
			return penaltyRuns;
		}
		public void setPenaltyRuns(int penaltyRuns) {
			this.penaltyRuns = penaltyRuns;
		}
		public int getBatsmanRuns() {
			return batsmanRuns;
		}
		public void setBatsmanRuns(int batsmanRuns) {
			this.batsmanRuns = batsmanRuns;
		}
		public int getExtraRuns() {
			return extraRuns;
		}
		public void setExtraRuns(int extraRuns) {
			this.extraRuns = extraRuns;
		}
		public int getTotalRuns() {
			return totalRuns;
		}
		public void setTotalRuns(int totalRuns) {
			this.totalRuns = totalRuns;
		}
		public String getBattingTeam() {
			return battingTeam;
		}
		public void setBattingTeam(String battingTeam) {
			this.battingTeam = battingTeam;
		}
		public String getBowlingTeam() {
			return bowlingTeam;
		}
		public void setBowlingTeam(String bowlingTeam) {
			this.bowlingTeam = bowlingTeam;
		}
		public String getBatsman() {
			return batsman;
		}
		public void setBatsman(String batsman) {
			this.batsman = batsman;
		}
		public String getNonStriker() {
			return nonStriker;
		}
		public void setNonStriker(String nonStriker) {
			this.nonStriker = nonStriker;
		}
		public String getBowler() {
			return bowler;
		}
		public void setBowler(String bowler) {
			this.bowler = bowler;
		}
		public String getPlayerDismissed() {
			return playerDismissed;
		}
		public void setPlayerDismissed(String playerDismissed) {
			this.playerDismissed = playerDismissed;
		}
		public String getDismissalKind() {
			return dismissalKind;
		}
		public void setDismissalKind(String dismissalKind) {
			this.dismissalKind = dismissalKind;
		}
		public String getFielder() {
			return fielder;
		}
		public void setFielder(String fielder) {
			this.fielder = fielder;
		}

	}

     
     
}