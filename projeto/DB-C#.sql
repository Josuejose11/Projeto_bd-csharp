CREATE DATABASE IF NOT EXISTS controle_despesas;
USE controle_despesas;
CREATE TABLE IF NOT EXISTS despesas (
    id_dps INT AUTO_INCREMENT PRIMARY KEY,
    titulo_dps VARCHAR(150),
    descricao_dps VARCHAR(300),
    valor_dps DECIMAL(10,2),
    categoria_dps VARCHAR(150),
    data_dps DATETIME
);