package tetrisbbdd;
import java.sql.*;
import java.io.*;
import java.util.*;

public class TetrisBBDD {
    public static String extractValue(String json, String key) {
        /* " key " builds a pattern that searches for the name of the key
        *  : indicates that after the key, goes a :
        *  (\\s*\"?) --> \\s allows any quantity of spaces, \"? allows the value to be within " or not
        *  ([^\",}]+) captures any text until it finds a , or a }
        *  \"? allows the value to have the closing " or not
        */ 
        String pattern = "\"" + key + "\":(\\s*\"?)([^\",}]+)\"?";
        return json.replaceAll(".*" + pattern + ".*", "$2");
    }
    
    public static void main(String[] args) throws java.io.IOException, SQLException, ClassNotFoundException, InterruptedException {
        System.out.println("Está corriendo.");
        
        java.io.File game = new java.io.File("C:\\Users\\Argo\\AppData\\LocalLow\\DefaultCompany\\GeometriaDelCaosGameData.txt");
        
        long lastModified = game.lastModified();
        
        Class.forName("com.mysql.cj.jdbc.Driver");
        
        Connection connection = DriverManager.getConnection("jdbc:mysql://dam.inspedralbes.cat/Tetris_BBDD?autoReconnect=true", "Tetris_tetris", "OM8*TBV5yv2O");

        PreparedStatement insertGame = connection.prepareStatement("insert into GAME values (null, ?, ?, ?, ?, ?, ?)");
        PreparedStatement selectPlayer = connection.prepareStatement("select name from PLAYER where name = ?");
        PreparedStatement insertPlayer = connection.prepareStatement("insert into PLAYER values (?, ?, ?, 1)");
        PreparedStatement updatePlayer = connection.prepareStatement("update PLAYER set time_played = time_played + ?, lines_destroyed = lines_destroyed + ?, games_played = games_played + 1 where name = ?");

        //java.io.File data = new java.io.File("data.txt");
        
        //Statement statement = connection.createStatement();

        //statement.executeUpdate("insert into PLAYER (name) values (\"JDG\")");


        /*if(data.exists()) {
            System.out.println("File already exists");
        }*/

        /*java.io.PrintWriter output = new java.io.PrintWriter(data);

        output.print("John T Smith ");
        output.println(90);
        output.print("Eric K Jones ");
        output.println(85);

        output.close();*/

        while(true){
        Scanner input = new Scanner(game);

         if (game.lastModified() != lastModified) {
                while(input.hasNextLine()) {
                    String jsonLine = input.nextLine();
                    jsonLine = jsonLine.trim();
                            
                    String name = extractValue(jsonLine, "name");
                    int score = Integer.parseInt(extractValue(jsonLine, "score"));
                    float time_played = Float.parseFloat(extractValue(jsonLine, "time_played"));
                    int lines_destroyed = Integer.parseInt(extractValue(jsonLine, "lines_destroyed"));
                    boolean ghost_piece = Boolean.parseBoolean(extractValue(jsonLine, "ghost_piece"));
                    int level = Integer.parseInt(extractValue(jsonLine, "level"));
                    selectPlayer.setString(1, name);
                    ResultSet selectPlayerResult = selectPlayer.executeQuery();
                    if(!selectPlayerResult.next()) {
                        insertPlayer.setString(1, name);
                        insertPlayer.setFloat(2, time_played);
                        insertPlayer.setInt(3, lines_destroyed);
                        insertPlayer.executeUpdate();
                        System.out.println("Jugador insertado");
                    }
                    else {
                        updatePlayer.setFloat(1, time_played);
                        updatePlayer.setInt(2, lines_destroyed);
                        updatePlayer.setString(3, name);
                        updatePlayer.executeUpdate();
                        System.out.println("Jugador actualizado");
                    }
                    insertGame.setString(1, name);
                    insertGame.setInt(2, score);
                    insertGame.setFloat(3, time_played);
                    insertGame.setInt(4, lines_destroyed);
                    insertGame.setBoolean(5, ghost_piece);
                    insertGame.setInt(6, level);
                    insertGame.executeUpdate();
                    System.out.println(name + " " + score + " " + time_played + " " + lines_destroyed + " " + ghost_piece + " " + level);
                    System.out.println("Partida insertada");
                }
                input.close();
                lastModified = game.lastModified();
            }
        }
        connection.close();
    
    }
}
