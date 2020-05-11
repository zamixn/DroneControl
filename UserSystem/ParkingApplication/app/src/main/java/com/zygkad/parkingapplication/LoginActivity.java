package com.zygkad.parkingapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class LoginActivity extends AppCompatActivity {

    private EditText license_plate_text;
    private EditText mobile_number_text;

    public static String LicensePlate = "";
    public static String PhoneNumber = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        license_plate_text = findViewById(R.id.license_plate_number);
        mobile_number_text = findViewById(R.id.mobile_number);

        if(LicensePlate != "")
            license_plate_text.setText(LicensePlate);
        if(PhoneNumber != "")
            mobile_number_text.setText(PhoneNumber);

        Button submit = findViewById(R.id.submitInfoButton);
        final LoginActivity activity = this;
        submit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                LicensePlate = license_plate_text.getText().toString();
                PhoneNumber = mobile_number_text.getText().toString();
                Intent intent = new Intent(activity, IndexActivity.class);
                startActivity(intent);
            }
        });
    }

    @Override
    protected void onResume() {
        super.onResume();
        if(LicensePlate != "")
            license_plate_text.setText(LicensePlate);
        if(PhoneNumber != "")
            mobile_number_text.setText(PhoneNumber);
    }
}
