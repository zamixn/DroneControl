package com.zygkad.parkingapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class IndexActivity extends AppCompatActivity {

    private Button changeInfoButton;
    private Button fineButton;
    private Button newReservationButton;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_index);

        changeInfoButton = findViewById(R.id.change_info_button);
        fineButton = findViewById(R.id.fine_button);
        newReservationButton = findViewById(R.id.new_reservation_button);

        final IndexActivity activity = this;

        changeInfoButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
        fineButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(activity, FineActivity.class);
                startActivity(intent);
            }
        });
        newReservationButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(activity, NewReservationActivity.class);
                startActivity(intent);
            }
        });
    }
}
