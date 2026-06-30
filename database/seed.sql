INSERT INTO Directors (Name, Nationality, Age, Active) VALUES
('Christopher Nolan', 'British',       54, 1),
('Hayao Miyazaki',    'Japanese',      85, 1),
('Greta Gerwig',      'American',      42, 1),
('Bong Joon-ho',      'South Korean',  56, 1),
('Quentin Tarantino', 'American',      63, 1);

INSERT INTO Movies (Name, ReleaseYear, Genre, Duration, FKDirector) VALUES
('Inception',              2010, 'Sci-Fi',      '02:28:00', 1),
('Interstellar',           2014, 'Sci-Fi',      '02:49:00', 1),
('Oppenheimer',            2023, 'Drama',       '03:00:00', 1),
('The Dark Knight',        2008, 'Action',      '02:32:00', 1),
('Dunkirk',                2017, 'War',         '01:46:00', 1),

('Spirited Away',          2001, 'Animation',   '02:05:00', 2),
('Princess Mononoke',      1997, 'Animation',   '02:14:00', 2),
('My Neighbor Totoro',     1988, 'Animation',   '01:26:00', 2),
('Howl''s Moving Castle',  2004, 'Animation',   '01:59:00', 2),
('Ponyo',                  2008, 'Animation',   '01:41:00', 2),

('Lady Bird',              2017, 'Drama',       '01:34:00', 3),
('Little Women',           2019, 'Drama',       '02:15:00', 3),
('Barbie',                 2023, 'Comedy',      '01:54:00', 3),
('Nights and Weekends',    2008, 'Drama',       '01:20:00', 3),
('Frances Ha',             2012, 'Comedy',      '01:26:00', 3),

('Parasite',               2019, 'Thriller',    '02:12:00', 4),
('Snowpiercer',            2013, 'Sci-Fi',      '02:06:00', 4),
('The Host',               2006, 'Horror',      '02:00:00', 4),
('Mother',                 2009, 'Mystery',     '02:09:00', 4),
('Okja',                   2017, 'Adventure',   '02:01:00', 4),

('Pulp Fiction',           1994, 'Crime',       '02:34:00', 5),
('Reservoir Dogs',         1992, 'Crime',       '01:39:00', 5),
('Kill Bill: Vol. 1',      2003, 'Action',      '01:51:00', 5),
('Django Unchained',       2012, 'Western',     '02:45:00', 5),
('Once Upon a Time in Hollywood', 2019, 'Comedy', '02:41:00', 5);