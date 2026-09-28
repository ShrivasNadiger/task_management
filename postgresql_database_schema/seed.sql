-- Sample Data

INSERT INTO users (name, email)
VALUES ('Rahul Sharma', 'rahul@example.com'),
      ('Priya Patel', 'priya@example.com'),
      ('Arjun Kumar', 'arjun@example.com');

INSERT INTO tasks
    (title, description, status, priority, user_id)
VALUES ('Learn PostgreSQL','Complete PostgreSQL fundamentals','COMPLETED','HIGH',1),
        ('Learn C#','Learn C# and OOP fundamentals','IN_PROGRESS','HIGH',1),
        ('Learn Vue.js','Learn Vue fundamentals','PENDING','MEDIUM',2),
        ('Build Task Dashboard','Create the Vue task management interface','PENDING','MEDIUM',2);