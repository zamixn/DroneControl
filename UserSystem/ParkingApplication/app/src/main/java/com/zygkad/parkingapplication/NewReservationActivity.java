package com.zygkad.parkingapplication;

import androidx.annotation.RequiresApi;
import androidx.appcompat.app.AppCompatActivity;

import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TimePicker;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.zygkad.parkingapplication.models.ParkingLot;

import java.io.IOException;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

import okhttp3.Call;
import okhttp3.Callback;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

public class NewReservationActivity extends AppCompatActivity {

    private TimePicker timePicker;
    private Spinner parkingLotSpinner;

    List<String> items;
    ArrayAdapter<String> adapter;
    Gson gson = new Gson();

    NewReservationActivity activity = this;

    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_reservation);

        timePicker = findViewById(R.id.timePicker);
        timePicker.setIs24HourView(true);
        timePicker.setHour(0);
        timePicker.setMinute(0);

        parkingLotSpinner = findViewById(R.id.parkingLotSpinner);
        items = new ArrayList<>();
        adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, items);
        parkingLotSpinner.setAdapter(adapter);
        run("https://78.58.170.115:3030/api/parkingLot");
    }

    void run(String url) {
        Request request = new Request.Builder()
                .url(url)
                .build();

        IndexActivity.client.newCall(request).enqueue(new Callback() {
            @Override
            public void onFailure(Call call, IOException e) {
                e.printStackTrace();
            }

            @Override
            public void onResponse(Call call, final Response response) throws IOException {
                if (!response.isSuccessful()) {
                    throw new IOException("Unexpected code " + response);
                } else {
                    Type listType = new TypeToken<List<ParkingLot>>(){}.getType();
                    List<ParkingLot> lots = gson.fromJson(response.body().string(), listType);

                    items.add("-");
                    for (ParkingLot l: lots ) {
                        items.add(l.address);
                        activity.runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                adapter.notifyDataSetChanged();
                            }
                        });
                        Log.d("response", l.address);
                    }
                }
            }
        });
    }
}
