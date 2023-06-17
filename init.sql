-- Check if the database already exists
-- DO $$ 
-- BEGIN
--     IF NOT EXISTS (SELECT FROM pg_database WHERE datname = 'nagpg') THEN
--         -- Create the database if it doesn't exist
--         CREATE DATABASE nagpg;
--         RAISE NOTICE 'Database "nagpg" created.';
--     ELSE
--         RAISE NOTICE 'Database "nagpg" already exists.';
--     END IF;
-- END $$;

-- Connect to the database
CREATE DATABASE nagpg;

\c nagpg

-- Create a table within the database
CREATE TABLE IF NOT EXISTS public."Products"
(
    "Id" uuid NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Price" numeric NOT NULL,
    "Description" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "PK_Products" PRIMARY KEY ("Id")
);

INSERT INTO public."Products" ("Id", "Name", "Price", "Description")
SELECT '34846ff2-368e-4ca3-9992-4846f0702342', 'IPhone13', '70000', 'A great phone with one of the best cameras'
WHERE NOT EXISTS (
    SELECT 1
    FROM public."Products"
    WHERE "Id" = '34846ff2-368e-4ca3-9992-4846f0702342'
)

