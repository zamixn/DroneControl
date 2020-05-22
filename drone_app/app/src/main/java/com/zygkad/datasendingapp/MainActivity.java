package com.zygkad.datasendingapp;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;
public class MainActivity extends AppCompatActivity {

    private Button sendDataButton;
    private EditText dataEditText;
    private TextView responseTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        sendDataButton = findViewById(R.id.button_send_data);
        dataEditText = findViewById(R.id.dataEditText);
        responseTextView = findViewById(R.id.responseTextView);
        sendDataButton.setOnClickListener(SendData());
    }

    private View.OnClickListener SendData(){
        return new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final String data = dataEditText.getText().toString();
                Thread t = new Thread(){
                    @Override
                    public void run() {
                        try {
                            Socket s = new Socket("rpissh.sytes.net", 808);
                            DataOutputStream dos = new DataOutputStream(s.getOutputStream());
                            dos.writeUTF(data);

                            //read input stream
                            DataInputStream dis2 = new DataInputStream(s.getInputStream());
                            InputStreamReader disR2 = new InputStreamReader(dis2);
                            BufferedReader br = new BufferedReader(disR2);//create a BufferReader object for input

                            String responseBuilder = "";
                            String line;
                            while ((line = br.readLine()) != null) {
                                responseBuilder += line;
                            }
                            final String response = responseBuilder;
                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    dataReceived(response);
                                }
                            });

                            dis2.close();
                            s.close();

                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                    }
                };
                t.start();
                Toast.makeText(MainActivity.this, "sending data...", Toast.LENGTH_SHORT).show();
            }
        };
    }

    private void dataReceived(String response){
        Toast.makeText(MainActivity.this, "data sent!", Toast.LENGTH_SHORT).show();
        responseTextView.setText("Received\n" + (response != null ? response : "null"));
    }
}
