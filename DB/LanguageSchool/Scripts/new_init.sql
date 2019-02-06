-- role

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (1001, N'Secretary', N'Sekretariat')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (1002, N'Teacher', N'Nauczyciel')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (1003, N'Student', N'Student')

-- typy wiadomości

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2001, N'', N'Do konkretnego użytkownika')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2002, N'', N'Do członków grupy')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2003, N'', N'Do uczestników kursu')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2004, N'', N'Do wszystkich użytkowników o roli')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2005, N'', N'Do wszystkich')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (2006, N'', N'Wiadomość powitalna dla studentów')

-- oceny

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3001, '', N'niedostateczny')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3002, '', N'dopuszczający')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3003, '', N'dostateczny')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3004, '', N'dobry')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3005, '', N'bardzo dobry')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (3006, '', N'celujący')

-- poziomy zaawansowania kursów

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4001, N'A1', N'A1', N'początkujący', N'beginner')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4002, N'A2', N'A2', N'niższy srednio zaawansowany', N'elementary/pre-intermediate')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4003, N'B1', N'B1', N'średnio zaawansowany', N'intermediate')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4004, N'B2', N'B2', N'wyższy srednio zaawansowany', N'upper/post-intermediate')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4005, N'C1', N'C1', N'zaawansowany', N'advanced')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName], [PLDescription], [ENDescription]) VALUES (4006, N'C2', N'C2', N'profesjonalny', N'nearly native-speaker level')

-- dni tygodnia

INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5001, N'Monday', N'Poniedziałek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5002, N'Tuesday', N'Wtorek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5003, N'Wednesday', N'Środa')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5004, N'Thursday', N'Czwartek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5005, N'Friday', N'Piątek')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5006, N'Saturday', N'Sobota')
INSERT INTO [Administration].[DictionaryItems]([Id], [ENName], [PLName]) VALUES (5007, N'Sunday', N'Niedziela')