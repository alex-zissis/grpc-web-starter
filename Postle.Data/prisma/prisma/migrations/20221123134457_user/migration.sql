CREATE OR REPLACE FUNCTION generate_object_id() RETURNS varchar AS $$
DECLARE
    time_component bigint;
    machine_id bigint := FLOOR(random() * 16777215);
    process_id bigint;
    seq_id bigint := FLOOR(random() * 16777215);
    result varchar:= '';
BEGIN
    SELECT FLOOR(EXTRACT(EPOCH FROM clock_timestamp())) INTO time_component;
    SELECT pg_backend_pid() INTO process_id;

    result := result || lpad(to_hex(time_component), 8, '0');
    result := result || lpad(to_hex(machine_id), 6, '0');
    result := result || lpad(to_hex(process_id), 4, '0');
    result := result || lpad(to_hex(seq_id), 6, '0');
    RETURN result;
END;
$$ LANGUAGE PLPGSQL;

-- CreateEnum
CREATE TYPE "UserStatus" AS ENUM ('INACTIVE', 'INVITED', 'ACTIVE');

-- CreateTable
CREATE TABLE "Account" (
    "account_id" TEXT NOT NULL DEFAULT generate_object_id(),
    "name" TEXT NOT NULL,

    CONSTRAINT "Account_pkey" PRIMARY KEY ("account_id")
);

-- CreateTable
CREATE TABLE "User" (
    "user_id" TEXT NOT NULL DEFAULT generate_object_id(),
    "email" TEXT NOT NULL,
    "password" TEXT NOT NULL,
    "first_name" TEXT NOT NULL,
    "last_name" TEXT NOT NULL,

    CONSTRAINT "User_pkey" PRIMARY KEY ("user_id")
);

-- CreateTable
CREATE TABLE "UserAssociations" (
    "user_id" TEXT NOT NULL,
    "account_id" TEXT NOT NULL,
    "assigned_at" TIMESTAMP(3) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "status" "UserStatus" NOT NULL DEFAULT 'INVITED',

    CONSTRAINT "UserAssociations_pkey" PRIMARY KEY ("user_id","assigned_at")
);

-- CreateIndex
CREATE UNIQUE INDEX "User_email_key" ON "User"("email");

-- AddForeignKey
ALTER TABLE "UserAssociations" ADD CONSTRAINT "UserAssociations_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "User"("user_id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "UserAssociations" ADD CONSTRAINT "UserAssociations_account_id_fkey" FOREIGN KEY ("account_id") REFERENCES "Account"("account_id") ON DELETE RESTRICT ON UPDATE CASCADE;
