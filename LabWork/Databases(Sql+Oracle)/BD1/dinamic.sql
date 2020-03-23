set serveroutput on;

select c.column_name into v_column
from user_constraint u, user_columns c
where u.contraint_name=c.constraint_name
and u.contraint_type='P'
and u.table_name=tableName;