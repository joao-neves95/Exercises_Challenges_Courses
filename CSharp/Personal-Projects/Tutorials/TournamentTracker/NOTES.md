# NOTES

## DATA

### Team

- TeamMembers (List<Person>)
- TeamName (string)

### Person

- FirstName (string)
- LastName (string)
- EmailAddress (string)
- CellphoneNumber (string)

### Tournament

- TournamentName (string)
- EntryFee (decimal)
- EnteredTeams (List<Team>)
- Prizes (List<Prize>)
- Rounds (List<List<Matchup>>) <!-- Matchup: Team vs.Team -->

### Prize

- PlaceNumber (int)
- PlaceName (string)
- PrizeAmount (decimal)
- PricePercentage (double)

### Matchup

- Entries (List<MatchupEntry>)
- Winner (Team)
- MatchupRound (int)

### MatchupEntry

- TeamCompetiong (Team)
- Score (double)
- ParentMatchup (Matchup)

[NEXT: Lesson 4]
