-- Minimal insert-only seed data for demo (current month/year: 2026-04)
-- Target: modernization PostgreSQL schema
-- No CREATE/ALTER/UPDATE statements included.

BEGIN;

INSERT INTO "Employees" ("EmpId", "FirstName", "LastName", "Addre", "City", "ZipCode", "Telephone", "Email", "AdmiDate", "Salary", "Password", "DeptId") VALUES
  (1001, 'Ariana', 'Cole', '45 Skyline Ave', 'Paris', '75010', '0701234567', 'ariana.cole.demo@airlines.local', '2026-04-02', 5200.00, 'demo123', 7)
ON CONFLICT ("EmpId") DO NOTHING;

INSERT INTO "Passengers" ("ClientId", "FirstName", "LastName", "Address", "City", "Country", "ZipCode", "Telephone", "Email") VALUES
  (1001, 'Noah', 'Bennett', '12 River Road', 'Lyon', 'France', '69002', '0612345678', 'noah.bennett.demo@example.com')
ON CONFLICT ("ClientId") DO NOTHING;

INSERT INTO "Shifts" ("ShiftId", "ShiftDate", "BeginTime", "EndTime", "CrewId") VALUES
  (1001, '2026-04-15', '09:00:00', '13:30:00', 1)
ON CONFLICT ("ShiftId") DO NOTHING;

INSERT INTO "Flights" ("FlightId", "FlightDate", "DepTime", "ArrTime", "TotPass", "TotBagga", "FlightNum", "ShiftId", "AirplaneId", "AirportDepId", "AirportArrId") VALUES
  (1001, '2026-04-15', '09:30:00', '12:45:00', 120, 8, 'CB2604', 1001, 2, 1, 3)
ON CONFLICT ("FlightId") DO NOTHING;

INSERT INTO "Buys" ("BuyId", "BuyDate", "BuyTime", "Price", "EmpId", "ClientId") VALUES
  (1001, '2026-04-10', '11:20:00', 149.99, 1001, 1001)
ON CONFLICT ("BuyId") DO NOTHING;

INSERT INTO "Tickets" ("TicketId", "BuyId", "ClientId", "FlightId", "Seat") VALUES
  (1001, 1001, 1001, 1001, 'C12')
ON CONFLICT ("TicketId") DO NOTHING;

COMMIT;
